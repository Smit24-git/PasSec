using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Queries.GetSecurityQAsByStorageId
{
    public class GetSecurityQAsByStorageIdQuery:IRequest<GetSecurityQAsByStorageIdQueryResponse>
    {
        public string VaultStorageKeyId { get; set; } = string.Empty;
        public string? UserKey {  get; set; }
    }
}
