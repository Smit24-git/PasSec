using MediatR;
using Microsoft.Extensions.Configuration;
using PasSecWebApi.Repositories.Contracts.Infrastructure;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using PasSecWebApi.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Commands.UpdateStorageKey
{
    public class UpdateStorageKeyCommandHandler : IRequestHandler<UpdateStorageKeyCommand, UpdateStorageKeyCommandResponse>
    {
        private readonly IDataEncryptor _encryptor;
        private readonly string _key;
        private readonly IVaultStorageKeyRepository _storageRepository;
        private readonly IVaultRepository _vaultRepository;

        public UpdateStorageKeyCommandHandler(
            IDataEncryptor encryptor, 
            IConfiguration configuration,
            IVaultStorageKeyRepository storageRepository,
            IVaultRepository vaultRepository)
        {
            _encryptor = encryptor;
            _key = configuration["ApiSettings:Secret"]!;
            _storageRepository = storageRepository;
            _vaultRepository = vaultRepository;
        }

        public async Task<UpdateStorageKeyCommandResponse> Handle(UpdateStorageKeyCommand request, CancellationToken cancellationToken)
        {
            var res = new UpdateStorageKeyCommandResponse();

            var validator = await new UpdateStorageKeyCommandValidator().ValidateAsync(request, cancellationToken);
            if(!validator.IsValid)
                throw new ValidationException(validator.Errors.Select(e=>e.ErrorMessage).ToList());

            var storageKey = (await _storageRepository.ListAllStorageKeysbyFilterAsync((k) => k.VaultStorageKeyId == new Guid(request.VaultStorageKeyId)))
                .FirstOrDefault() ?? throw new BadRequestException(["Invalid Request."]);

            var vault = (await _vaultRepository.ListAllByFilterAsync(v=>v.VaultId == storageKey.VaultId)).FirstOrDefault()
                ?? throw new Exception("Missing Vault Details.");

            var securityKey = _key;
            var IV = storageKey.IV;
            if (vault.AppliedCustomKey)
            {
                if (string.IsNullOrEmpty(request.UserKey))
                    throw new ValidationException(["UserKey is Required."]);

                securityKey = request.UserKey;
            }

            storageKey.KeyName = request.KeyName;
            
            storageKey.Username = string.IsNullOrWhiteSpace(request.Username)
                ? request.Username
                : _encryptor.EncryptValue(securityKey, IV, request.Username).Item1;

            storageKey.Password = _encryptor.EncryptValue(securityKey, IV, request.Password).Item1;
            
            storageKey.EmailAddress = string.IsNullOrWhiteSpace(request.Email)
                ? request.Email
                : _encryptor.EncryptValue(securityKey, IV, request.Email).Item1;

            storageKey.AccessLocation = string.IsNullOrWhiteSpace(request.AccessLocation)
                ? request.AccessLocation
                : _encryptor.EncryptValue(securityKey, IV, request.AccessLocation).Item1;

            await _storageRepository.UpdateAsync(storageKey);

            res.IsSuccess = true;
            return res;
        }
    }
}
