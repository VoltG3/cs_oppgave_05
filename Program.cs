using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using cs_oppgave_05.Infrastructure.Diagnostics;

using cs_oppgave_05.Infrastructure.Presistance;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace cs_oppgave_05;

public partial class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("Default")
                               ?? throw new InvalidOperationException("Connection string 'Default' not found.");

        // Test SQL Connection
        MysqlConnectionTester.AssertCanOpen(connectionString);
        
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
        
        // Search default files
        app.UseDefaultFiles(new DefaultFilesOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Assets"))
        });

        // Static files from /Assets
        var provider = new FileExtensionContentTypeProvider();
        provider.Mappings[".ico"] = "image/x-icon";

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Assets")),
            RequestPath = "",
            ContentTypeProvider = provider
        });

        // Path to /favicon.ico
        app.MapGet("/favicon.ico", async ctx =>
        {
            ctx.Response.ContentType = "image/x-icon";
            ctx.Response.Headers.CacheControl = "no-store";
            await ctx.Response.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "favicon.ico"));
        });
        
        app.UseCors("DefaultCors");
        app.UseAuthorization();

        app.MapControllers();
        app.MapGet("/ping", () => "Ping OK");
        app.MapGet("/", () => Results.Content(
            """
            <!doctype html>
            <html lang="en">
              <head>
                <meta charset="utf-8">
                <title>API Movies</title>
                <link rel="icon" href="/favicon.ico">
              </head>
              <body>
              
                <h1>API Movie</h1>
                <p>Try <a href="/api/Movies">Movies</a></p>
                <p>Try <a href="/api/Directors">Directors</a></p>
                <p>Try <a href="/api/MovieDirection">/api/MovieDirection</a></p>
                <p>Try <a href="/api/Actors">/api/Actors</a></p>
                <p>Try <a href="/api/MovieCasts">/api/MovieCasts</a></p>
                <p>Try <a href="/api/Genres">/api/Genres</a></p>
                <p>Try <a href="/api/MovieGenres">/api/MovieGenres</a></p>
                <p>Try <a href="/api/Reviewers">/api/Reviewers</a></p>
                <p>Try <a href="/api/Ratings?movId=925&revId=9021">/api/Ratings</a></p>
                
              </body>
            </html>
            """,
            "text/html"
        ));
        app.Run();
    }
}
