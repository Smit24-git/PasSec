using PasSecWebApi.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Commands.AddSecurityQA
{
    public class AddSecurityQACommandResponse:BaseResponse
    {
        public Guid Id { get; set; }
    }
}
