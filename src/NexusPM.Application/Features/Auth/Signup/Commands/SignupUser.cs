using MediatR;
using Microsoft.EntityFrameworkCore;
using NexusPM.Application.Common.Exceptions;
using NexusPM.Application.Common.Interfaces;
using NexusPM.Domain.Events;

namespace NexusPM.Application.Features.Commands;

/// <summary>
/// Command to sign up a new user with the specified email and display name.
/// </summary>
public sealed record SignupUserCommand(string Email, string Displayname)
    : IRequest<SignupUserResponse>;

/// <summary>
/// Response indicating the result of the signup operation.
/// </summary>
public sealed record SignupUserResponse(bool IsSuccess);

/// <summary>
/// Handles the <see cref="SignupUserCommand"/> to register a new user.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SignupUserCommandHandler"/> class.
/// </remarks>
/// <param name="nexusDb">The Nexus database context.</param>
public class SignupUserCommandHandler(INexusDbContext nexusDb)
    : IRequestHandler<SignupUserCommand, SignupUserResponse>
{
    /// <inheritdoc />
    public async Task<SignupUserResponse> Handle(SignupUserCommand request, CancellationToken cancellationToken)
    {
        var isEmailExisted = await nexusDb.Users.AsNoTracking()
            .AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (isEmailExisted)
        {
            throw new DuplicateEmailException(request.Email);
        }

        var newUser = new AppUser
        {
            Email = request.Email,
            DisplayName = request.Displayname,
        };

        newUser.AddDomainEvent(new SignedUpEvent(newUser));
        nexusDb.Users.Add(newUser);
        await nexusDb.SaveChangesAsync(cancellationToken);

        return new SignupUserResponse(true);
    }
}