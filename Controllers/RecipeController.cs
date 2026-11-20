using System.Text;
using MySqlConnector;
using SmartRecipeBook.Data;
using SmartRecipeBook.Models;

namespace SmartRecipeBook.Controllers;

public class RecipeController
{
    private const string SelectRecipe =
        "SELECT r.id, r.owner_id, r.title, r.instructions, r.photo_path, r.status, u.full_name " +
        "FROM recipes r JOIN users u ON u.id = r.owner_id";

    private static Recipe Map(MySqlDataReader rd) => new()
    {
        Id = rd.GetInt32(0), OwnerId = rd.GetInt32(1), Title = rd.GetString(2),
        Instructions = rd.GetString(3), PhotoPath = rd.IsDBNull(4) ? null : rd.GetString(4),
        Status = rd.GetString(5), OwnerName = rd.GetString(6)
    };

    // scope: "published", "mine", "favorites", "pending"
    public List<Recipe> List(string scope, string search, int userId)
    {
        var list = new List<Recipe>();
        if (scope == "pending" && Session.Current?.IsAdmin != true) return list;

        var sql = new StringBuilder(SelectRecipe + " WHERE ");
        sql.Append(scope switch
        {
            "mine" => "r.owner_id = @uid",
            "favorites" => "r.status = 'PUBLISHED' AND r.id IN (SELECT recipe_id FROM favorites WHERE user_id = @uid)",
            "pending" => "r.status = 'PENDING'",
            _ => "r.status = 'PUBLISHED'"
        });
        var args = new List<(string, object?)>();
        if (scope is "mine" or "favorites") args.Add(("uid", userId));
        if (!string.IsNullOrWhiteSpace(search))
        {
            sql.Append(" AND (LOWER(r.title) LIKE @q OR EXISTS (SELECT 1 FROM recipe_ingredients i " +
                       "WHERE i.recipe_id = r.id AND LOWER(i.name) LIKE @q))");
            args.Add(("q", "%" + search.Trim().ToLower() + "%"));
        }
        sql.Append(" ORDER BY r.id DESC");

        using var conn = Db.Open();
        using var cmd = Db.Command(conn, sql.ToString(), args.ToArray());
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) list.Add(Map(rd));
        return list;
    }

    public Recipe? Get(int id)
    {
        using var conn = Db.Open();
        Recipe? r;
        using (var cmd = Db.Command(conn, SelectRecipe + " WHERE r.id = @id", ("id", id)))
        using (var rd = cmd.ExecuteReader())
        {
            if (!rd.Read()) return null;
            r = Map(rd);
        }
        using var ic = Db.Command(conn,
            "SELECT name, quantity FROM recipe_ingredients WHERE recipe_id = @id ORDER BY id", ("id", id));
        using var ir = ic.ExecuteReader();
        while (ir.Read())
            r.Ingredients.Add(new Ingredient { Name = ir.GetString(0), Quantity = ir.GetString(1) });
        return r;
    }

    // Creates or updates a recipe. submit=true sends it for admin review.
    // Returns null on success, or an error message.
    public string? Save(Recipe r, bool submit, User me)
    {
        r.Title = r.Title.Trim();
        r.Instructions = r.Instructions.Trim();
        if (r.Title.Length == 0 || r.Title.Length > 150) return "Title is required (max 150 characters).";
        if (r.Instructions.Length == 0 || r.Instructions.Length > 2000) return "Instructions are required (max 2000 characters).";
        if (r.Ingredients.Any(i => i.Name.Length > 80 || i.Quantity.Length > 60))
            return "Ingredient name max 80 and quantity max 60 characters.";
        if (submit && r.Ingredients.Count == 0) return "Add at least one ingredient before submitting.";

        try
        {
            using var conn = Db.Open();
            using var tx = conn.BeginTransaction();
            var status = submit ? "PENDING" : "DRAFT";
            var photo = string.IsNullOrWhiteSpace(r.PhotoPath) ? null : r.PhotoPath;
            int id = r.Id;

            if (id == 0)
            {
                using var ins = Db.TxCommand(conn, tx,
                    "INSERT INTO recipes (owner_id, title, instructions, photo_path, status) " +
                    "VALUES (@o, @t, @i, @p, @s)",
                    ("o", me.Id), ("t", r.Title), ("i", r.Instructions), ("p", photo), ("s", status));
                ins.ExecuteNonQuery();
                id = (int)ins.LastInsertedId;
            }
            else
            {
                using var upd = Db.TxCommand(conn, tx,
                    "UPDATE recipes SET title = @t, instructions = @i, photo_path = @p, status = @s " +
                    "WHERE id = @id AND owner_id = @o AND status IN ('DRAFT','REJECTED')",
                    ("t", r.Title), ("i", r.Instructions), ("p", photo), ("s", status),
                    ("id", id), ("o", me.Id));
                if (upd.ExecuteNonQuery() == 0) return "This recipe can no longer be edited.";
                using var del = Db.TxCommand(conn, tx,
                    "DELETE FROM recipe_ingredients WHERE recipe_id = @id", ("id", id));
                del.ExecuteNonQuery();
            }

            foreach (var ing in r.Ingredients)
            {
                using var insIng = Db.TxCommand(conn, tx,
                    "INSERT INTO recipe_ingredients (recipe_id, name, quantity) VALUES (@r, @n, @q)",
                    ("r", id), ("n", ing.Name), ("q", ing.Quantity));
                insIng.ExecuteNonQuery();
            }

            if (submit)
            {
                using var note = Db.TxCommand(conn, tx,
                    "INSERT INTO notifications (user_id, message) SELECT id, @m FROM users WHERE role = 'ADMIN'",
                    ("m", $"New recipe waiting for review: {r.Title}"));
                note.ExecuteNonQuery();
            }

            tx.Commit();
            return null;
        }
        catch (MySqlException ex) { return "Database error: " + ex.Message; }
    }

    public string? Delete(int id, User me)
    {
        try
        {
            using var conn = Db.Open();
            using var sel = Db.Command(conn, "SELECT owner_id FROM recipes WHERE id = @id", ("id", id));
            var owner = sel.ExecuteScalar();
            if (owner == null) return "Recipe not found.";
            if (Convert.ToInt32(owner) != me.Id && !me.IsAdmin) return "You can only delete your own recipes.";
            using var del = Db.Command(conn, "DELETE FROM recipes WHERE id = @id", ("id", id));
            del.ExecuteNonQuery();
            return null;
        }
        catch (MySqlException ex) { return "Database error: " + ex.Message; }
    }

    public string? Review(int id, bool approve, User me)
    {
        if (!me.IsAdmin) return "Only an admin can review recipes.";
        try
        {
            using var conn = Db.Open();
            using var tx = conn.BeginTransaction();

            using var upd = Db.TxCommand(conn, tx,
                "UPDATE recipes SET status = @s WHERE id = @id AND status = 'PENDING'",
                ("s", approve ? "PUBLISHED" : "REJECTED"), ("id", id));
            if (upd.ExecuteNonQuery() == 0) return "That recipe is not waiting for review.";

            int owner; string title;
            using (var sel = Db.TxCommand(conn, tx, "SELECT owner_id, title FROM recipes WHERE id = @id", ("id", id)))
            using (var rd = sel.ExecuteReader())
            {
                rd.Read();
                owner = rd.GetInt32(0);
                title = rd.GetString(1);
            }
            using var note = Db.TxCommand(conn, tx,
                "INSERT INTO notifications (user_id, message) VALUES (@u, @m)",
                ("u", owner), ("m", $"Your recipe \"{title}\" was {(approve ? "approved" : "rejected")}."));
            note.ExecuteNonQuery();

            tx.Commit();
            return null;
        }
        catch (MySqlException ex) { return "Database error: " + ex.Message; }
    }

    public string ToggleFavorite(int userId, int recipeId)
    {
        try
        {
            using var conn = Db.Open();
            using var del = Db.Command(conn,
                "DELETE FROM favorites WHERE user_id = @u AND recipe_id = @r",
                ("u", userId), ("r", recipeId));
            if (del.ExecuteNonQuery() > 0) return "Removed from favorites.";

            using var chk = Db.Command(conn,
                "SELECT COUNT(*) FROM recipes WHERE id = @r AND status = 'PUBLISHED'", ("r", recipeId));
            if (Convert.ToInt32(chk.ExecuteScalar()) == 0) return "Only published recipes can be favorites.";

            using var ins = Db.Command(conn, "INSERT INTO favorites (user_id, recipe_id) VALUES (@u, @r)",
                ("u", userId), ("r", recipeId));
            ins.ExecuteNonQuery();
            return "Added to favorites.";
        }
        catch (MySqlException ex) { return "Database error: " + ex.Message; }
    }

    // Returns unread notifications and marks them read.
    public List<string> TakeNotifications(int userId)
    {
        var list = new List<string>();
        using var conn = Db.Open();
        using (var cmd = Db.Command(conn,
            "SELECT message FROM notifications WHERE user_id = @u AND is_read = 0 ORDER BY id", ("u", userId)))
        using (var rd = cmd.ExecuteReader())
            while (rd.Read()) list.Add(rd.GetString(0));
        using var upd = Db.Command(conn, "UPDATE notifications SET is_read = 1 WHERE user_id = @u AND is_read = 0",
            ("u", userId));
        upd.ExecuteNonQuery();
        return list;
    }
}
