using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Commands.UpdateSecurityQA
{
    public class UpdateSecurityQACommand:IRequest<UpdateSecurityQACommandResponse>
    {
        public string? UserKey {  get; set; }
        public string Id { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
