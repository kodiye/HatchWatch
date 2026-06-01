using MySqlConnector;

namespace HatchWatch.DAL;

public static class VeritabaniBaglanti
{
    private static readonly string connectionString =
        "Server=localhost;Port=3306;Database=hatchwatch_db;Uid=root;Pwd=MYSQL_SIFRESI;";

    public static MySqlConnection BaglantiGetir()
    {
        return new MySqlConnection(connectionString);
    }
}
