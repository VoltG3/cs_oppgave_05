using MySqlConnector;
using Spectre.Console;

namespace cs_oppgave_05.Infrastructure.Diagnostics;

public static class MysqlConnectionTester
{
    /// <summary>
    /// Tries to open a MySQL connection and prints a green success or red failure message.
    /// Throws on failure (so CI can fail), but also prints a readable error.
    /// </summary>
    public static void AssertCanOpen(string connectionString)
    {
        try
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open(); // or: await conn.OpenAsync();

            AnsiConsole.MarkupLine("[bold green]MySQL connection successful.[/]");
        }
        catch (Exception ex)
        {
            // Escape to avoid Spectre markup conflicts with special chars in the message
            var msg = Markup.Escape(ex.Message);
            AnsiConsole.MarkupLine($"[bold red]MySQL connection failed:[/] {msg}");
            throw; // keep throwing so the caller/CI knows it failed
        }
    }
    
}
