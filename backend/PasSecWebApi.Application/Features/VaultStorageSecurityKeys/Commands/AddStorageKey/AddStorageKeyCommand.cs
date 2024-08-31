using MediatR;
using PasSecWebApi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Commands.AddStorageKey
{
    public class AddStorageKeyCommand:IRequest<AddStorageKeyCommandResponse>
    {
        public string? UserKey { get; set; } = string.Empty;
        public string VaultId { get; set; } = string.Empty;
        public string KeyName { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? AccessLocation { get; set; }
        
        public List<AddStorageKeyQA>? SecurityQAs { get; set; }
    }

    public class AddStorageKeyQA
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
