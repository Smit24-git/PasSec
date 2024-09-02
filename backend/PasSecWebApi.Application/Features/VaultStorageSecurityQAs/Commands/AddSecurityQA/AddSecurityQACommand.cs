using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Commands.AddSecurityQA
{
    public class AddSecurityQACommand:IRequest<AddSecurityQACommandResponse>
    {
        public string? UserKey { get; set; }
        public string Question {  get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string KeyId { get; set; } = string.Empty;
    }
}
