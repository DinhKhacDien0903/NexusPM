using DotNetEnv;
using NexusPM.API.Hosting;
using NexusPM.Infrastructure;
using NexusPM.Infrastructure.Boot;

namespace NexusPM.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddOpenApi();

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddAPIService();

        Env.Load();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        //await app.ApplyMigrationAsync();

        app.MapGet("/health", () => Results.Ok("OK"));

        await app.RunAsync();
    }
}
