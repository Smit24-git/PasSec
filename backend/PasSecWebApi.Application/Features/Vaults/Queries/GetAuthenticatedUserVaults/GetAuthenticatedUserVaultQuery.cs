using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Vaults.Queries.GetAuthenticatedUserVaults
{
    public class GetAuthenticatedUserVaultQuery:IRequest<GetAuthenticatedUserVaultQueryResponse>
    {
    }
}
