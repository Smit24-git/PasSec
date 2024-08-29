using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Vaults.Queries.GetVaultById
{
    public class GetVaultByIdQuery:IRequest<GetVaultByIdQueryResponse>
    {
        public string VaultId { get; set; } = string.Empty;

        public string? UserKey { get; set; }

    }
}
