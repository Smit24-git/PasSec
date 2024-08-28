using PasSecWebApi.Application.Responses;
using PasSecWebApi.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Vaults.Queries.GetAuthenticatedUserVaults
{
    public class GetAuthenticatedUserVaultQueryResponse:BaseResponse
    {
        public List<VaultDto> Vaults { get; set; } = [];
    }
}
