namespace NexusPM.Infrastructure.Data.Interceptors;
public class DesignTimeTenantProvider : ITenantProvider
{
    public static readonly DesignTimeTenantProvider Instance = new ();

    private DesignTimeTenantProvider()
        => this.TenantId = Guid.TryParse(Environment.GetEnvironmentVariable("TENANT_ID"), out var id) ? id : Guid.Empty;

    public Guid TenantId { get; }
}
