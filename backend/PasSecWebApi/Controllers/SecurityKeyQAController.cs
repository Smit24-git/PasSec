using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Commands.AddSecurityQA;
using PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Commands.UpdateSecurityQA;
using PasSecWebApi.Shared.Exceptions;

namespace PasSecWebApi.Controllers
{
    [ApiController]
    public class SecurityKeyQAController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SecurityKeyQAController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/security-key-qas")]
        public async Task<ActionResult<AddSecurityQACommandResponse>> CreateSecurityKeyQA(AddSecurityQACommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        [HttpPut]
        [Route("api/security-key-qas/{id}")]
        public async Task<ActionResult<UpdateSecurityQACommandResponse>> CreateSecurityKeyQA(string id, UpdateSecurityQACommand cmd)
        {
            if(id != cmd.Id)
                throw new BadRequestException(["Invalid Request."]);

            return Ok(await _mediator.Send(cmd));
        }
    }
}
