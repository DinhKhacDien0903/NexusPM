namespace NexusPM.Domain.Enums;

/// <summary>
/// Represents the roles a user can have in a project.
/// </summary>
public enum ProjectRole
{
    /// <summary>
    /// A user who can view the project but cannot make changes.
    /// </summary>
    Viewer = 0,

    /// <summary>
    /// A user who can contribute to the project by making changes.
    /// </summary>
    Contributor = 1,

    /// <summary>
    /// A user who maintains the project and has elevated permissions
    /// </summary>
    Maintainer = 2,

    /// <summary>
    /// A user who manages the project and has the highest level of permissions.
    /// </summary>
    Manager = 3,
}
