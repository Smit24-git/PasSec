using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Vaults.Commands.UpdateVault
{
    public class UpdateVaultCommand:IRequest<UpdateVaultCommandResponse>
    {
        public string VaultId { get; set; } = string.Empty;
        public string VaultName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
