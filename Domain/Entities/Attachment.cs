namespace NexusPM.Domain.Entities;
public class Attachment : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid IssueId { get; set; }

    public Issue? Issue { get; set; }

    public string FileName { get; set; } = default!;

    public string ContentType { get; set; } = default!;

    public long SizeBytes { get; set; }

    public string StorageKey { get; set; } = default!;
}
