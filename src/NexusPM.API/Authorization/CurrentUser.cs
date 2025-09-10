using System;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using NexusPM.Application.Abstractions.Security;

namespace NexusPM.API.Authorization;

public class CurrentUser(IHttpContextAccessor http)
    : ICurrentUser
{
    private readonly IHttpContextAccessor http = http;

    public bool IsAuthenticated => this.http.HttpContext?.User?.Identity?.IsAuthenticated == true;
    public Guid UserId => this.TryGuid(JwtRegisteredClaimNames.Sub) ?? Guid.Empty;
    public Guid TenantId => this.TryGuid("tid") ?? Guid.Empty;
    public string? Email => this.http.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Email);
    public string? TenantRole => this.http.HttpContext?.User?.FindFirstValue("trole");
    public string? SessionId => this.http.HttpContext?.User?.FindFirstValue("sid");
    private Guid? TryGuid(string type)
    {
        var v = this.http.HttpContext?.User?.FindFirstValue(type);
        return Guid.TryParse(v, out var g) ? g : null;
    }
}