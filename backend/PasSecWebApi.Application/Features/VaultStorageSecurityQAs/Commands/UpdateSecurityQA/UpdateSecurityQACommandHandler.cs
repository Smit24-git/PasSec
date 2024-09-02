using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using PasSecWebApi.Repositories.Contracts.Infrastructure;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using PasSecWebApi.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Commands.UpdateSecurityQA
{
    public class UpdateSecurityQACommandHandler : IRequestHandler<UpdateSecurityQACommand, UpdateSecurityQACommandResponse>
    {
        private readonly IVaultStorageKeySecurityQARepository _qaRepository;
        private readonly IVaultStorageKeyRepository _vaultStorageKeyRepository;
        private readonly IVaultRepository _vaultRepository;
        private readonly IDataEncryptor _encryptor;
        private readonly string _key;

        public UpdateSecurityQACommandHandler(
            IVaultStorageKeySecurityQARepository qaRepository, 
            IVaultStorageKeyRepository vaultStorageKeyRepository,
            IVaultRepository vaultRepository, 
            IDataEncryptor encryptor, 
            IConfiguration configuration)
        {
            _qaRepository = qaRepository;
            _vaultRepository = vaultRepository;
            _encryptor = encryptor;
            _vaultStorageKeyRepository = vaultStorageKeyRepository;
            _key = configuration["ApiSettings:Secret"]!;
        }

        public async Task<UpdateSecurityQACommandResponse> Handle(UpdateSecurityQACommand request, CancellationToken cancellationToken)
        {
            var res = new UpdateSecurityQACommandResponse();

            var validator = await (new UpdateSecurityQACommandValidator()).ValidateAsync(request, cancellationToken);
            if (!validator.IsValid)
                throw new ValidationException(validator.Errors.Select(x => x.ErrorMessage).ToList());


            var qa = (await _qaRepository.ListAllSequrityQAbyFilterAsync(x => x.VaultStorageKeySecurityQAId == new Guid(request.Id)))
                .FirstOrDefault() ?? throw new BadRequestException(["Invalid Request."]);
            
            var vsKey = (await _vaultStorageKeyRepository.ListAllStorageKeysbyFilterAsync(x=>x.VaultStorageKeyId == qa.VaultStorageKeyId))
                .FirstOrDefault() ?? throw new BadRequestException(["Invalid Request."]);

            var vault = (await _vaultRepository.ListAllByFilterAsync(x=>x.VaultId == vsKey.VaultId))
                .FirstOrDefault() ?? throw new BadRequestException(["Invalid Request."]);

            var securityKey = _key;
            var IV = qa.IV;
            if (vault.AppliedCustomKey)
            {
                if (string.IsNullOrWhiteSpace(request.UserKey))
                {
                    throw new ValidationException(["User Key is required."]);
                }
                securityKey = request.UserKey;
            }

            qa.Question = _encryptor.EncryptValue(securityKey, IV, request.Question).Item1;
            qa.Answer = _encryptor.EncryptValue(securityKey, IV, request.Answer).Item1;

            await _qaRepository.UpdateAsync(qa);

            res.Id = qa.VaultStorageKeyId;
            res.IsSuccess = true;
            return res;
        }
    }
}
