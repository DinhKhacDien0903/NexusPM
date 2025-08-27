namespace NexusPM.Domain.Enums;

/// <summary>
/// Represents the role of a tenant in the system.
/// </summary>
public enum TenantRole
{
    /// <summary>
    /// The owner of the tenant.
    /// </summary>
    Owner = 0,

    /// <summary>
    /// An administrator with elevated permissions.
    /// </summary>
    Admin = 1,

    /// <summary>
    /// A regular member of the tenant.
    /// </summary>
    Member = 2,

    /// <summary>
    /// Responsible for billing-related tasks.
    /// </summary>
    Biling = 3,
}
