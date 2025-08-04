using MySql.Data.MySqlClient;

namespace cs_oppgave_05.tests.MYSQLconnection
{
    public static class MysqlConnectionTester
    {
        public static void TestConnection()
        {
            string connectionCredentials = "" +
                                           "server=localhost;" +
                                           "user=oppgave_05;" +
                                           "database=mydb;" +
                                           "port=3307;" +
                                           "password=password";

            try
            {
                using var connection = new MySqlConnection(connectionCredentials);
                connection.Open();
                Console.WriteLine("MYSQL Connection successful");
            }
            catch (Exception e)
            {
                Console.WriteLine("MYSQL Connection failed");
                Console.WriteLine(e);
                throw;
            }
        }
    }
}   