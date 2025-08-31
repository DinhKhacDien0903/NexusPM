namespace NexusPM.Domain.Common;
public interface ITenantScoped
{
    Guid TenantId { get; set; }
}
