namespace NexusPM.Domain.Enums;

/// <summary>
/// Represents the state of a sprint in the project management lifecycle.
/// </summary>
public enum SprintState
{
    /// <summary>
    /// The sprint is planned but has not started yet.
    /// </summary>
    Planned = 0,

    /// <summary>
    /// The sprint is currently active and in progress.
    /// </summary>
    Active = 1,

    /// <summary>
    /// The sprint has been completed.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// The sprint has been archived and is no longer active.
    /// </summary>
    Archived = 3,
}
