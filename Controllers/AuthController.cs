using MySqlConnector;
using SmartRecipeBook.Data;
using SmartRecipeBook.Models;

namespace SmartRecipeBook.Controllers;

public static class Session
{
    public static User? Current { get; set; }
}

public class AuthController
{
    // Returns null on success, or an error message.
    public string? Register(string fullName, string email, string password)
    {
        fullName = fullName.Trim();
        email = email.Trim().ToLowerInvariant();
        if (fullName.Length == 0 || fullName.Length > 100) return "Enter your full name (max 100 characters).";
        if (!email.Contains('@') || email.Length > 150) return "Enter a valid email address.";
        if (password.Length < 8) return "Password must be at least 8 characters.";
        try
        {
            using var conn = Db.Open();
            using var count = Db.Command(conn, "SELECT COUNT(*) FROM users");
            // The very first account becomes the admin.
            var role = Convert.ToInt32(count.ExecuteScalar()) == 0 ? "ADMIN" : "USER";
            using var ins = Db.Command(conn,
                "INSERT INTO users (full_name, email, password_hash, role) VALUES (@n, @e, @h, @r)",
                ("n", fullName), ("e", email),
                ("h", BCrypt.Net.BCrypt.HashPassword(password)), ("r", role));
            ins.ExecuteNonQuery();
            return null;
        }
        catch (MySqlException ex) when (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
        {
            return "That email is already registered.";
        }
        catch (MySqlException ex) { return "Database error: " + ex.Message; }
    }

    public User? Login(string email, string password)
    {
        using var conn = Db.Open();
        using var cmd = Db.Command(conn,
            "SELECT id, full_name, email, password_hash, role FROM users WHERE email = @e",
            ("e", email.Trim().ToLowerInvariant()));
        using var rd = cmd.ExecuteReader();
        if (!rd.Read()) return null;
        if (!BCrypt.Net.BCrypt.Verify(password, rd.GetString(3))) return null;
        return new User
        {
            Id = rd.GetInt32(0), FullName = rd.GetString(1),
            Email = rd.GetString(2), Role = rd.GetString(4)
        };
    }
}
