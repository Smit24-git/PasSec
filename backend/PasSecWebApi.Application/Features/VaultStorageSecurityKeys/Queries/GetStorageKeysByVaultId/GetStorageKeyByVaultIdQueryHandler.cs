using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Queries.GetSecurityQAsByStorageId;
using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories.Contracts.Infrastructure;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using PasSecWebApi.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Queries.GetStorageKeysByVaultId
{
    public class GetStorageKeyByVaultIdQueryHandler : IRequestHandler<
        GetStorageKeyByVaultIdQuery,
        GetStorageKeyByVaultIdQueryResponse>
    {
        private readonly string _key;
        private readonly IVaultStorageKeyRepository _repo;
        private readonly IMapper _mapper;
        private readonly IDataDecryptor _decryptor;
        private readonly IMediator _mediatr;

        public GetStorageKeyByVaultIdQueryHandler(
            IConfiguration configuration, 
            IVaultStorageKeyRepository repo, 
            IMapper mapper, 
            IDataDecryptor decryptor,
            IMediator mediatr)
        {
            _key = configuration["ApiSettings:Secret"]!;
            _repo = repo;
            _mapper = mapper;
            _decryptor = decryptor;
            _mediatr = mediatr;
        }

        public async Task<GetStorageKeyByVaultIdQueryResponse> Handle(GetStorageKeyByVaultIdQuery request, CancellationToken cancellationToken)
        {
            var res = new GetStorageKeyByVaultIdQueryResponse();
            res.VaultStorageKeys = [];
            var encryptionKey = string.IsNullOrEmpty(request.UserKey) ? _key : request.UserKey;
            var keys = await _repo.ListAllStorageKeysbyFilterAsync((v) => v.VaultId== new Guid(request.VaultId));
            foreach (var key in keys)
            {
                key.Username = string.IsNullOrEmpty(key.Username) ? key.Username : _decryptor.DecryptCipher(encryptionKey, key.IV, key.Username);
                key.Password = _decryptor.DecryptCipher(encryptionKey, key.IV, key.Password);
                key.EmailAddress = string.IsNullOrEmpty(key.EmailAddress) ? key.EmailAddress : _decryptor.DecryptCipher(encryptionKey, key.IV, key.EmailAddress);
                key.AccessLocation = string.IsNullOrEmpty(key.AccessLocation)? key.AccessLocation :  _decryptor.DecryptCipher(encryptionKey, key.IV, key.AccessLocation);
                key.IV = "";
                
                var keyDto = _mapper.Map<VaultStorageKeyDto>(key);
                GetSecurityQAsByStorageIdQueryResponse keyQAs = await _mediatr.Send(new GetSecurityQAsByStorageIdQuery
                {
                    UserKey = request.UserKey,
                    VaultStorageKeyId = key.VaultStorageKeyId.ToString(),
                }, cancellationToken);
                keyDto.SecurityQAs = keyQAs.SecurityQAs;
                res.VaultStorageKeys.Add(keyDto);
            }
            res.IsSuccess = true;
            return res;
        }
    }
}
