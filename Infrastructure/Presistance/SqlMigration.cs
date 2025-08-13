using MySqlConnector;
using Spectre.Console;

namespace cs_oppgave_05.Infrastructure.Presistance;

public static class SqlMigration
{
    public static void Run(string connectionString)
    {
        string scriptsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Infrastructure", "Presistance", "Migrations", "SqlScripts");

        if (!Directory.Exists(scriptsDirectory))
        {
            AnsiConsole.MarkupLine("[bold red]SQL script folder not found[/]");
            return;
        }

        var sqlFiles = Directory.GetFiles(scriptsDirectory, "*.sql").OrderBy(f => f);

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using (var cmd = new MySqlCommand(
            @"CREATE TABLE IF NOT EXISTS __migrations (
                ScriptName VARCHAR(255) PRIMARY KEY,
                ExecutedAt DATETIME DEFAULT CURRENT_TIMESTAMP
              );", connection))
        {
            cmd.ExecuteNonQuery();
        }

        foreach (var file in sqlFiles)
        {
            string fileName = Path.GetFileName(file);

            using var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM __migrations WHERE ScriptName = @name", connection);
            checkCmd.Parameters.AddWithValue("@name", fileName);

            var alreadyExecuted = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
            if (alreadyExecuted)
            {
                AnsiConsole.MarkupLine($"[bold green]Skipping already executed script:[/][bold yellow] { fileName }[/]");
                continue;
            }

            string sql = File.ReadAllText(file);
            try
            {
                using var execCmd = new MySqlCommand(sql, connection);
                execCmd.ExecuteNonQuery();

                using var insertCmd = new MySqlCommand("INSERT INTO __migrations (ScriptName) VALUES (@name)", connection);
                insertCmd.Parameters.AddWithValue("@name", fileName);
                insertCmd.ExecuteNonQuery();
                
                AnsiConsole.MarkupLine($"[bold green]Executed:[/][bold cyan] { fileName }[/]");
               
                AnsiConsole.Write(new Panel(sql)
                    .Header($"[grey]SQL content of {fileName}[/]")
                    .Collapse()
                    .RoundedBorder()
                    .BorderColor(Color.Grey));
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[bold red]Error in { fileName }: { ex.Message }[/]");
            }
        }
    }
}
