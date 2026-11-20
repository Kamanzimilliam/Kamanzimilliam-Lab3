using MySqlConnector;

namespace SmartRecipeBook.Data;

public static class Db
{
    // Matches the user created by database/schema.sql.
    // If you use a different MySQL user/password/port, change it here.
    private const string ConnectionString =
        "Server=localhost;Port=3307;Database=smart_recipe_book;User ID=recipe;Password=Recipe123;";

    public static MySqlConnection Open()
    {
        var conn = new MySqlConnection(ConnectionString);
        conn.Open();
        return conn;
    }

    public static MySqlCommand Command(MySqlConnection conn, string sql,
        params (string Name, object? Value)[] args)
    {
        var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        foreach (var (name, value) in args)
            cmd.Parameters.AddWithValue("@" + name, value ?? DBNull.Value);
        return cmd;
    }

    // Same as Command, but the command takes part in an open transaction.
    public static MySqlCommand TxCommand(MySqlConnection conn, MySqlTransaction tx, string sql,
        params (string Name, object? Value)[] args)
    {
        var cmd = Command(conn, sql, args);
        cmd.Transaction = tx;
        return cmd;
    }
}
