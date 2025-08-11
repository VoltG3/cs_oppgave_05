using System.Text.Json.Serialization;
using cs_oppgave_05.Tests.MYSQLConnection;
using cs_oppgave_05.Migration.MYSQLMigration;
using cs_oppgave_05.Data;
using Microsoft.EntityFrameworkCore;


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
        SqlMigration.Run(connectionString);
        
        var builder = WebApplication.CreateBuilder(args);
        
        // To reduce serialization infinitiv loop issue
        builder.Services.AddControllers()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
        
        // WebServer
        builder.Services.AddControllers();
        
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));
        
        // CORS preflight
        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy("DefaultCors", p => p
                    .WithOrigins("http://localhost:5000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
        });
        
        var app = builder.Build();

        app.UseAuthorization();
        app.UseCors("AllowAll");
        
        app.MapControllers();
        app.MapGet("/", () => "Server OK");
        app.MapGet("/ping", () => "Ping OK");
        
        app.Run();
        
    }
}
