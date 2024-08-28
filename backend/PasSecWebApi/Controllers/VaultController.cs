using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasSecWebApi.Application.Features.Vaults.Commands.CreateVault;

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
        public async Task<ActionResult> CreateVault(CreateVaultCommand cmd)
        {
            return Ok(await _mediatr.Send(cmd));
        }
    }
}
