using PasSecWebApi.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Vaults.Commands.UpdateVault
{
    public class UpdateVaultCommandResponse:BaseResponse
    {
        public Guid VaultId { get; set; }
        public string VaultName { get; set; } = string.Empty;
        public string? Description { get; set;} = string.Empty;
    }
}
