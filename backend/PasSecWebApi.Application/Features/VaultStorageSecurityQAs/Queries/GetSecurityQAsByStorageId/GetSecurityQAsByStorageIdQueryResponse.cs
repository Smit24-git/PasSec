using PasSecWebApi.Application.Responses;
using PasSecWebApi.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Queries.GetSecurityQAsByStorageId
{
    public class GetSecurityQAsByStorageIdQueryResponse:BaseResponse
    {
        public List<VaultStorageKeySecurityQADto> SecurityQAs { get; set; } = [];
    }
}
