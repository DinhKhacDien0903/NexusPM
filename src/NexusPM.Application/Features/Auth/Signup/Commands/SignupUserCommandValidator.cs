namespace NexusPM.Application.Features.Commands;

/// <summary>
/// Validator for <see cref="SignupUserCommand"/>. Ensures that the email and display name are provided and valid.
/// </summary>
public class SignupUserCommandValidator : AbstractValidator<SignupUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SignupUserCommandValidator"/> class.
    /// </summary>
    public SignupUserCommandValidator()
    {
        this.RuleFor(x => x.Email)
            .MaximumLength(256).WithMessage("Email must not exceed 256 characters.")
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        this.RuleFor(x => x.Displayname)
            .MaximumLength(120).WithMessage("Display Name must not exceed 120 characters.")
            .NotEmpty().WithMessage("Display name is required.");
    }
}