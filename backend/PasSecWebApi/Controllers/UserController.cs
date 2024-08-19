using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasSecWebApi.Application.Features.Users.Commands.LoginUser;
using PasSecWebApi.Application.Features.Users.Commands.RegisterUser;

namespace PasSecWebApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/users/register")]
        public async Task<ActionResult<RegisterUserResponse>> RegisterUser(RegisterUserCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpPost]
        [Route("api/users/login")]
        public async Task<ActionResult<LoginUserResponse>> LoginUser(LoginUserCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}
