using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Commands.AddStorageKey;

namespace PasSecWebApi.Controllers
{
    [ApiController]
    public class VaultStorageSecurityKeyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VaultStorageSecurityKeyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [Route("api/vault-storage-security-keys")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<AddStorageKeyCommandResponse>>
            AddSecurityStorageKey(AddStorageKeyCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}
