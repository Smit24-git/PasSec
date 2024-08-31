using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasSecWebApi.Application.Features.Vaults.Commands.CreateVault;
using PasSecWebApi.Application.Features.Vaults.Commands.UpdateVault;
using PasSecWebApi.Application.Features.Vaults.Queries.GetAuthenticatedUserVaults;
using PasSecWebApi.Application.Features.Vaults.Queries.GetVaultById;
using PasSecWebApi.Shared.Exceptions;

namespace PasSecWebApi.Controllers
{
    [ApiController]
    public class VaultController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public VaultController(IMediator mediator)
        {
            _mediatr = mediator;
        }

        [HttpPost]
        [Authorize]
        [Route("api/vaults")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateVaultCommandResponse>> CreateVault(CreateVaultCommand cmd)
        {
            return Ok(await _mediatr.Send(cmd));
        }

        [HttpGet]
        [Authorize]
        [Route("api/vaults")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateVaultCommandResponse>> GetUserVaults()
        {
            return Ok(await _mediatr.Send(new GetAuthenticatedUserVaultQuery()));
        }

        [HttpPost]
        [Authorize]
        [Route("api/vaults/{vaultId}")]
        public async Task<ActionResult> GetVaultDetails(string vaultId, [FromBody] GetVaultByIdQuery qry )
        {
            if(vaultId!= qry.VaultId)
            {
                throw new BadRequestException(["Invalid Request."]);
            }
            return Ok(await _mediatr.Send(qry));
        }

        [HttpPut]
        [Authorize]
        [Route("api/vaults/{vaultId}")]
        public async Task<ActionResult> UpdateVault(string vaultId, [FromBody] UpdateVaultCommand cmd)
        {
            if (vaultId != cmd.VaultId)
                throw new BadRequestException(["Invalid Request."]);
            
            return Ok(await _mediatr.Send(cmd));
        }
    }
}
