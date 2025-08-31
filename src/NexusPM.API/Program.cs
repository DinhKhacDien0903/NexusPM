using NexusPM.API.Hosting;
using NexusPM.Infrastructure;

namespace NexusPM.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddOpenApi();

        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        await app.ApplyMigrationAsync();

        app.MapGet("/health", () => Results.Ok("OK")); // check health endpoint

        await app.RunAsync();
    }
}
