using MySqlConnector;
using Spectre.Console;

namespace cs_oppgave_05.Tests.MYSQLConnection;

public static class MysqlConnectionTester
{
    public static void TestConnection(string connectionString ) 
    {
        try
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            AnsiConsole.MarkupLine("[bold green]MYSQL Connection successful[/]");
        }
        
        catch (Exception e)
        {
            AnsiConsole.MarkupLine("[bold red]MYSQL Connection failed[/]");
            Console.WriteLine(e);
            throw;
        }
    }
}
 