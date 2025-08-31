namespace NexusPM.Domain.Entities;
public class IssueTag : ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid IssueId { get; set; }

    public Guid TagId { get; set; }

    public Issue? Issue { get; set; }

    public Tag? Tag { get; set; }
}
