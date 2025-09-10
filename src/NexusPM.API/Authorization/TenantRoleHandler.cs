using Microsoft.AspNetCore.Authorization;
using NexusPM.Domain.Enums;
using NexusPM.Infrastructure.Data.Interceptors;

namespace NexusPM.API.Authorization;

/// <summary>
/// Handles authorization requirements for tenant-based roles.
/// </summary>
/// <param name="tenantProvider">The tenant provider to get current tenant information.</param>
public class TenantRoleHandler(ITenantProvider tenantProvider)
    : AuthorizationHandler<TenantRoleRequirement>
{
    private readonly ITenantProvider tenantProvider = tenantProvider;

    /// <summary>
    /// Handles the authorization requirement by checking if the user has the required tenant role.
    /// </summary>
    /// <param name="context">The authorization context containing user claims.</param>
    /// <param name="requirement">The tenant role requirement to evaluate.</param>
    /// <returns>A completed task.</returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TenantRoleRequirement requirement)
    {
        if (context.User?.Identity?.IsAuthenticated != true)
        {
            return Task.CompletedTask;
        }

        var claimTid = context.User.FindFirst("tid")?.Value;
        var claimRoleStr = context.User.FindFirst("trole")?.Value;
        var currentTid = this.tenantProvider.TenantId.ToString();

        if (claimTid == currentTid
            && Enum.TryParse<TenantRole>(claimRoleStr, true, out var userRole)
            && RoleHierarchy.Rank(userRole) >= RoleHierarchy.Rank(requirement.MinimumRole))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
