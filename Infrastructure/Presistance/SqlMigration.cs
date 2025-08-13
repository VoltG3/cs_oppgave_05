using MySqlConnector;
using Spectre.Console;
using System.Text;

namespace cs_oppgave_05.Infrastructure.Presistance;

public static class SqlMigration
{
    // New helper method: Reads connection string from IConfiguration and runs SQL scripts
    public static void RunFromConfig(IConfiguration config,
                                     string connName = "Default",
                                     string? scriptsPath = null,
                                     string? contentRoot = null)
    {
        var cs = config.GetConnectionString(connName)
                 ?? throw new InvalidOperationException($"Connection string '{connName}' not found.");
        RunInternal(cs, scriptsPath, contentRoot);
    }

    // New helper method: Runs SQL scripts directly from a WebApplicationBuilder instance
    // // (convenient because it already has both Configuration and Environment)
    public static void RunFromBuilder(WebApplicationBuilder builder,
                                      string connName = "Default",
                                      string? scriptsPath = "Infrastructure/Presistance/Migrations/SqlScripts")
    {
        RunFromConfig(builder.Configuration, connName, scriptsPath, builder.Environment.ContentRootPath);
    }
    
    private static void RunInternal(string connectionString, string? scriptsPath, string? contentRoot)
    {
        var scriptsDirectory = ResolveScriptsDir(scriptsPath, contentRoot);
        if (scriptsDirectory is null)
        {
            AnsiConsole.MarkupLine("[bold red]SQL script folder not found[/]");
            return;
        }

        var sqlFiles = Directory.EnumerateFiles(scriptsDirectory, "*.sql")
                                .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
                                .ToArray();

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        EnsureMigrationsTable(connection);

        foreach (var file in sqlFiles)
        {
            var fileName = Path.GetFileName(file);
            if (AlreadyExecuted(connection, fileName))
            {
                AnsiConsole.MarkupLine($"[green]Skipping already executed script:[/] [yellow]{fileName}[/]");
                continue;
            }

            var sql = File.ReadAllText(file, Encoding.UTF8);
            try
            {
                using var cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                MarkExecuted(connection, fileName);

                AnsiConsole.MarkupLine($"[bold green]Executed:[/] [cyan]{fileName}[/]");
                AnsiConsole.Write(new Panel(sql)
                    .Header($"[grey]SQL content of {fileName}[/]")
                    .Collapse()
                    .RoundedBorder()
                    .BorderColor(Color.Grey));
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[bold red]Error in {fileName}: {ex.Message}[/]");
            }
        }
    }

    private static string? ResolveScriptsDir(string? scriptsPath, string? contentRoot)
    {
        if (!string.IsNullOrWhiteSpace(scriptsPath))
        {
            var baseDir = !string.IsNullOrWhiteSpace(contentRoot) ? contentRoot : Directory.GetCurrentDirectory();
            var combined = Path.GetFullPath(Path.Combine(baseDir, scriptsPath));
            if (Directory.Exists(combined)) return combined;
        }
        
        var candidates = new[]
        {
            Path.Combine(contentRoot ?? Directory.GetCurrentDirectory(), "Infrastructure", "Presistance", "Migrations"),
            Path.Combine(AppContext.BaseDirectory, "Infrastructure", "Presistance", "Migrations"),
            Path.Combine(Directory.GetCurrentDirectory(), "Infrastructure", "Presistance", "Migrations"),
            Path.Combine(AppContext.BaseDirectory, "Migrations"),
            Path.Combine(Directory.GetCurrentDirectory(), "Migrations"),
        };

        foreach (var c in candidates)
            if (Directory.Exists(c)) return c;

        return null;
    }

    private static void EnsureMigrationsTable(MySqlConnection connection)
    {
        using var cmd = new MySqlCommand(
            @"CREATE TABLE IF NOT EXISTS __migrations (
                ScriptName VARCHAR(255) PRIMARY KEY,
                ExecutedAt DATETIME DEFAULT CURRENT_TIMESTAMP
              );", connection);
        cmd.ExecuteNonQuery();
    }

    private static bool AlreadyExecuted(MySqlConnection connection, string fileName)
    {
        using var checkCmd = new MySqlCommand(
            "SELECT COUNT(*) FROM __migrations WHERE ScriptName = @name", connection);
        checkCmd.Parameters.AddWithValue("@name", fileName);
        return Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
    }

    private static void MarkExecuted(MySqlConnection connection, string fileName)
    {
        using var insertCmd = new MySqlCommand(
            "INSERT INTO __migrations (ScriptName) VALUES (@name)", connection);
        insertCmd.Parameters.AddWithValue("@name", fileName);
        insertCmd.ExecuteNonQuery();
    }
}
