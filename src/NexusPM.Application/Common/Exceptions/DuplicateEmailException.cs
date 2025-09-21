namespace NexusPM.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when attempting to register a user with an email that already exists.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DuplicateEmailException"/> class.
/// </remarks>
/// <param name="email">The duplicate email address.</param>
public sealed class DuplicateEmailException(string email)
    : Exception($"A user with email '{email}' already exists.")
{
    /// <summary>
    /// Gets the duplicate email address.
    /// </summary>
    public string Email { get; } = email;
}