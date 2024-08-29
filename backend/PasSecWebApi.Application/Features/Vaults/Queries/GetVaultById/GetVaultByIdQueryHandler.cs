using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Queries.GetStorageKeysByVaultId;
using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using PasSecWebApi.Shared.Dtos;
using PasSecWebApi.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Vaults.Queries.GetVaultById
{
    public class GetVaultByIdQueryHandler : IRequestHandler<GetVaultByIdQuery, GetVaultByIdQueryResponse>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IVaultRepository _vaultRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _key;

        public GetVaultByIdQueryHandler(
            IHttpContextAccessor contextAccessor, 
            IVaultRepository vaultRepository, 
            IMapper mapper,
            IConfiguration _conf,
            IMediator mediator)
        {
            _contextAccessor = contextAccessor;
            _vaultRepository = vaultRepository;
            _mapper = mapper;
            _key = _conf["ApiSettings:Secret"]!;
            _mediator = mediator;
        }

        public async Task<GetVaultByIdQueryResponse> Handle(GetVaultByIdQuery request, CancellationToken cancellationToken)
        {
            var res =  new GetVaultByIdQueryResponse();

            var validator = new GetVaultByIdQueryValidator();
            var validationRes = await validator.ValidateAsync(request, cancellationToken);
            if (!validationRes.IsValid)
                throw new ValidationException(validationRes.Errors.Select(x=>x.ErrorMessage).ToList());

            var currentUserId = _contextAccessor.HttpContext.User.Identity!.Name; 
            var vaultId = new Guid(request.VaultId);
            Expression<Func<Vault,bool>> filterForVaultCheck = (v) => v.AddedBy == currentUserId && v.VaultId == vaultId;
            var vaults = await _vaultRepository.ListAllByFilterAsync(filterForVaultCheck);
            var vault = vaults.FirstOrDefault()
                ?? throw new BadRequestException(["Vault Not Found."]);
            var encryptionKey = _key;
            var IV = vault.IV;

            if (vault.AppliedCustomKey)
            {
                if (string.IsNullOrEmpty(request.UserKey)) return res;

                encryptionKey = request.UserKey;
            }

            var vaultDto = _mapper.Map<VaultDto>(vault);
            


            GetStorageKeyByVaultIdQueryResponse storageKeys =  await _mediator.Send(new GetStorageKeyByVaultIdQuery
            {
                UserKey = encryptionKey,
                VaultId = vault.VaultId.ToString(),
            });

            vaultDto.StorageKeys = storageKeys.VaultStorageKeys;

            res.Vault = vaultDto;
            res.IsSuccess = true;
            return res;

        }
    }
}
