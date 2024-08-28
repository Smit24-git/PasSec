using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using PasSecWebApi.Shared.Dtos;

namespace PasSecWebApi.Application.Features.Vaults.Queries.GetAuthenticatedUserVaults
{
    public class GetAuthenticatedUserVaultQueryHandler : IRequestHandler<
        GetAuthenticatedUserVaultQuery,
        GetAuthenticatedUserVaultQueryResponse>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IVaultRepository _vaultRepository;
        private readonly IMapper _mapper;

        public GetAuthenticatedUserVaultQueryHandler(
            IHttpContextAccessor contextAccessor,
            IVaultRepository vaultRepository,
            IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _vaultRepository = vaultRepository;
            _mapper = mapper;
        }

        public async Task<GetAuthenticatedUserVaultQueryResponse> Handle(
            GetAuthenticatedUserVaultQuery request,
            CancellationToken cancellationToken
        )
        {
            var res = new GetAuthenticatedUserVaultQueryResponse();
            var userId = _contextAccessor.HttpContext.User.Identity!.Name;

            var userVaults = await _vaultRepository.ListAllByFilterAsync((v) => v.AddedBy == userId);
            res.Vaults = _mapper.Map<List<VaultDto>>(userVaults);
            res.IsSuccess = true;

            return res;
        }
    }
}
