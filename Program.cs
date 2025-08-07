using cs_oppgave_05.Tests.MYSQLConnection;
using cs_oppgave_05.MYSQLMigration;

//      Task: RestAPI with Controllers (MVC)
//
//      1.0 Lag en ny mappe Model
//      1.1 Lag en datamodell klasse
//      1.2 Valgfri (forsøk å legge til støtte for SQL i API-et)
//
//      2.0 Lag en controller som implementerer modellen deres
//      2.1 Lag et GET-endepunkt
//      2.2 Lag et POST-endepunkt


class Program
{
    
    static void Main(string[] args)
    {
        
        string connectionString = "" +
                                  "Server=localhost;" +
                                  "Port=3309;" +
                                  "Database=movies;" +
                                  "User=all;" +
                                  "Password=mysql;";
        
        Console.Clear();
        MysqlConnectionTester.TestConnection(connectionString);
        // if ok, then next:
        
        Migration.Run(connectionString);
        
        // WebServer
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Server OK");
        app.MapGet("/ping", () => "Ping OK");

        app.Run();
        
    }
}
