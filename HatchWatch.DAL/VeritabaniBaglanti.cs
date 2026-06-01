using MySqlConnector;

namespace HatchWatch.DAL;

public static class VeritabaniBaglanti
{
    public static MySqlConnection BaglantiGetir()
    {
        string mysqlSifresi = Environment.GetEnvironmentVariable("HATCHWATCH_MYSQL_PASSWORD") ?? "MYSQL_SIFRESI";
        string connectionString = $"Server=localhost;Port=3306;Database=hatchwatch_db;Uid=root;Pwd={mysqlSifresi};";

        return new MySqlConnection(connectionString);
    }
}
