using PasSecWebApi.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Commands.UpdateSecurityQA
{
    public class UpdateSecurityQACommandResponse:BaseResponse
    {
        public Guid Id { get; set; }

    }
}
