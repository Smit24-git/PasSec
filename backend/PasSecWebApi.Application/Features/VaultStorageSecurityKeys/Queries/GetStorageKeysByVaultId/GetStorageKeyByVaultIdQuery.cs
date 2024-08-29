using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Queries.GetStorageKeysByVaultId
{
    public class GetStorageKeyByVaultIdQuery:IRequest<GetStorageKeyByVaultIdQueryResponse>
    {
        public string VaultId { get; set; } = string.Empty;
        public string? UserKey { get; set; }
    }
}
