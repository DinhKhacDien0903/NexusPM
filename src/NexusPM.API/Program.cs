// Copyright (c) YourCompany. All rights reserved.

using NexusPM.API.Common.Exceptions;
using NexusPM.Application;
using NexusPM.Infrastructure;

namespace NexusPM.API;

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

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwagerGenDocumentation();

        builder.Services.AddOpenApi();

        builder.Services.AddApplication();

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddAPIService();

        builder.Services.AddJWTAuthentication(builder.Configuration);

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddProblemDetails();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        //app.UseMiddleware<MissingTenantMiddleware>();

        app.MapControllers();

        app.UseExceptionHandler();

        await app.RunAsync();
    }
}
