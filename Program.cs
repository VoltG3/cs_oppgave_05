using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using cs_oppgave_05.Tests.MYSQLConnection;
using cs_oppgave_05.Infrastructure.Presistance;

namespace cs_oppgave_05;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("Default")
                               ?? throw new InvalidOperationException("Connection string 'Default' not found.");

        // Test SQL Connection
        MysqlConnectionTester.TestConnection(connectionString);
        
        // SQL Migration
        SqlMigration.RunFromBuilder(builder, connName: "Default",
            scriptsPath: "Infrastructure/Presistance/Migrations/SqlScripts");

        // JSON
        builder.Services.AddControllers()
            .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

        // EF Core ar MySQL
        builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // CORS
        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy("DefaultCors", p => p
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
        });

        var app = builder.Build();

        app.UseCors("DefaultCors");
        app.UseAuthorization();

        app.MapControllers();
        app.MapGet("/", () => "Server OK");
        app.MapGet("/ping", () => "Ping OK");

        app.Run();
    }
}
