// Copyright (c) YourCompany. All rights reserved.
namespace NexusPM.API.Middlewares;
using NexusPM.Infrastructure.Data.Tenancy;

/// <summary>
/// Middleware to handle requests with missing or invalid tenant information.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MissingTenantMiddleware"/> class.
/// </remarks>
/// <param name="next">The next middleware in the pipeline.</param>
public sealed class MissingTenantMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate next = next;

    /// <summary>
    /// Invokes the middleware to process the HTTP context and resolve the tenant.
    /// </summary>
    /// <param name="ctx">The HTTP context of the current request.</param>
    /// <param name="tenantCtx">The tenant context to set the resolved tenant ID.</param>
    /// <param name="store">The tenant store to resolve tenant information.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Invoke(HttpContext ctx, RequestTenantContext tenantCtx, ITenantStore store)
    {
        var tid = await ResolveAsync(ctx, store, ctx.RequestAborted);
        if (tid == Guid.Empty)
        {
            ctx.Response.StatusCode = StatusCodes.Status404NotFound;
            await ctx.Response.WriteAsync("Missing or invalid tenant");
            return;
        }

        tenantCtx.Set(tid);
        try
        {
            await this.next(ctx);
        }
        finally
        {
            tenantCtx.Clear();
        }
    }

    /// <summary>
    /// Resolves the tenant ID from the HTTP context.
    /// </summary>
    /// <param name="context">The HTTP context of the current request.</param>
    /// <param name="store">The tenant store to resolve tenant information.</param>
    /// <param name="ct">The cancellation token for the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the resolved tenant ID.</returns>
    private static async Task<Guid> ResolveAsync(HttpContext context, ITenantStore store, CancellationToken ct)
    {
        if (context.Request.Headers.TryGetValue("X-TenantId", out var h) &&
            Guid.TryParse(h, out var g) && await store.ExistsAsync(g, ct))
        {
            return g;
        }

        if (context.Request.Headers.TryGetValue("X-Tenant", out var slug) && !string.IsNullOrWhiteSpace(slug))
        {
            var id = await store.ResolveBySlugAsync(slug!, ct);
            if (id.HasValue)
            {
                return id.Value;
            }
        }

        var host = context.Request.Host.Host;
        var parts = host.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 3)
        {
            var id = await store.ResolveBySlugAsync(parts[0], ct);
            if (id.HasValue)
            {
                return id.Value;
            }
        }

        return Guid.Empty;
    }
}
