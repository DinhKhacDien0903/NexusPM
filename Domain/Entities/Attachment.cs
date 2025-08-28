namespace NexusPM.Domain.Entities;
public class Attachment : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid IssueId { get; set; }

    public Issue? Issue { get; set; }

    [MaxLength(260)]
    public string FileName { get; set; } = default!;
    [MaxLength(120)]
    public string ContentType { get; set; } = default!;

    public long SizeBytes { get; set; }

    [MaxLength(500)]
    public string StorageKey { get; set; } = default!;
}
