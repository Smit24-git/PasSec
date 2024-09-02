using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories.Contracts.Infrastructure;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using PasSecWebApi.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Commands.AddSecurityQA
{
    public class AddSecurityQACommandHandler : IRequestHandler<AddSecurityQACommand, AddSecurityQACommandResponse>
    {
        private readonly IVaultStorageKeySecurityQARepository _qaRepository;
        private readonly IVaultStorageKeyRepository _vaultStorageKeyRepository;
        private readonly IVaultRepository _vaultRepository;
        private readonly IDataEncryptor _encryptor;
        private readonly string _key;
        private readonly IHttpContextAccessor _contextAccessor;

        public AddSecurityQACommandHandler(
            IVaultStorageKeySecurityQARepository qaRepository, 
            IVaultStorageKeyRepository vaultStorageKeyRepository, 
            IVaultRepository vaultRepository, 
            IDataEncryptor encryptor, 
            IHttpContextAccessor contextAccessor,
            IConfiguration configuration)
        {
            _qaRepository = qaRepository;
            _vaultStorageKeyRepository = vaultStorageKeyRepository;
            _vaultRepository = vaultRepository;
            _encryptor = encryptor;
            _key = configuration["ApiSettings:Secret"]!;
            _contextAccessor = contextAccessor;
        }

        public async Task<AddSecurityQACommandResponse> Handle(AddSecurityQACommand request, CancellationToken cancellationToken)
        {
            var res = new AddSecurityQACommandResponse();
            var userId = _contextAccessor.HttpContext.User.Identity!.Name!;
            var date = DateTime.Now;
            var securityKey = _key;
            string IV;
            var validator = await (new AddSecurityQACommandValidator()).ValidateAsync(request,cancellationToken);
            if(!validator.IsValid)
                throw new ValidationException(validator.Errors.Select(x=>x.ErrorMessage).ToList());

            var key = (await _vaultStorageKeyRepository.ListAllStorageKeysbyFilterAsync((k)=>k.VaultStorageKeyId == new Guid(request.KeyId)))
                .FirstOrDefault() ?? throw new BadRequestException(["Invalid Request."]);

            var vault = (await _vaultRepository.ListAllByFilterAsync(x => x.VaultId == key.VaultId))
                .FirstOrDefault() ?? throw new BadRequestException(["Invalid Request."]);

            IV = vault.IV;

            if (vault.AppliedCustomKey)
            {
                if (string.IsNullOrWhiteSpace(request.UserKey))
                {
                    throw new ValidationException(["User Key is Required"]);
                }
                securityKey = request.UserKey;
            }

            var Sqa = new VaultStorageKeySecurityQA
            {
                AddedAt = date,
                AddedBy = userId,
                IV = IV,
                VaultStorageKeyId = key.VaultStorageKeyId,
                Question = _encryptor.EncryptValue(securityKey, IV, request.Question).Item1,
                Answer = _encryptor.EncryptValue(securityKey, IV, request.Answer).Item1,
            };
            await _qaRepository.AddAsync(Sqa);
            
            res.Id = Sqa.VaultStorageKeySecurityQAId;
            res.IsSuccess = true;
            return res;
        }
    }
}
