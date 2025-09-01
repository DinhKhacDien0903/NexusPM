// Copyright (c) YourCompany. All rights reserved.

namespace NexusPM.API;
using DotNetEnv;
using NexusPM.API.Middlewares;
using NexusPM.Infrastructure;

/// <summary>
/// The entry point of the NexusPM.API application.
/// </summary>
public class Program
{
    /// <summary>
    /// The main method that serves as the entry point for the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
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

        app.UseMiddleware<MissingTenantMiddleware>();

        app.MapControllers();

        app.MapGet("/health", () => Results.Ok("OK"));

        await app.RunAsync();
    }
}
