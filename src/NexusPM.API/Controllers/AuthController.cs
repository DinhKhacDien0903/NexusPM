using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusPM.API.Common.Contracts;
using NexusPM.API.Dtos;
using NexusPM.Application.Features.Commands;

namespace NexusPM.API.Controllers
{
    /// <summary>
    /// Controller responsible for authentication-related actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </remarks>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator)
        : ControllerBase
    {
        /// <summary>
        /// Registers a new user with the provided sign-up information.
        /// </summary>
        /// <param name="request">The sign-up request containing user details.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>The result of the sign-up operation.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "email": "dinhkhacdien1009@gmail.com",
        ///        "displayName": "Dinh Khac Dien"
        ///     }.
        /// </remarks>
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignUpRequest request, CancellationToken ct)
        {
            var result = await mediator.Send(new SignupUserCommand(request.Email, request.DisplayName ?? string.Empty), ct);
            return this.CreatedAtAction(nameof(this.Signup), ApiResponseFactory.Ok(this.HttpContext, result));
        }
    }
}
