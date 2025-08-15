using MySqlConnector;

namespace cs_oppgave_05.Infrastructure.Diagnostics;

public static class MysqlConnectionTester
{
    public static void AssertCanOpen(string connectionString)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
    }
    
}
