using PasSecWebApi.Application.Responses;
using PasSecWebApi.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Queries.GetStorageKeysByVaultId
{
    public class GetStorageKeyByVaultIdQueryResponse:BaseResponse
    {
        public List<VaultStorageKeyDto> VaultStorageKeys { get; set; } = [];
    }
}
