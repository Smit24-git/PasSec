using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories.Contracts.Infrastructure;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using PasSecWebApi.Shared.Exceptions;

namespace PasSecWebApi.Application.Features.Vaults.Commands.CreateVault
{
    public class CreateVaultCommandHandler : IRequestHandler<CreateVaultCommand, CreateVaultCommandResponse>
    {

        private readonly IDataEncryptor _dataEncryptor;
        private readonly string _key;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IVaultRepository _vaultRepository;

        public CreateVaultCommandHandler(
            IDataEncryptor dataEncryptor, 
            IConfiguration configuration, 
            IHttpContextAccessor contextAccessor,
            IVaultRepository vaultRepository)
        {
            _dataEncryptor = dataEncryptor;
            _key = configuration["ApiSettings:Secret"]!;
            _contextAccessor = contextAccessor;
            _vaultRepository = vaultRepository;
        }

        async Task<CreateVaultCommandResponse> IRequestHandler<
            CreateVaultCommand,
            CreateVaultCommandResponse>
            .Handle(CreateVaultCommand request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.HttpContext.User.Identity!.Name!;

            var res = new CreateVaultCommandResponse();

            var validator = new CreateVaultCommandValidator();

            var validationRes = await validator.ValidateAsync(request, cancellationToken);
            if (!validationRes.IsValid)
                throw new ValidationException(validationRes.Errors.Select(x => x.ErrorMessage).ToList());

            var Vault = new Vault();
            var key = request.UseUserKey ? request.UserKey : _key; 
            var (vaultName, iv) = _dataEncryptor.EncryptValue(key, null, request.VaultName);
            Vault.VaultName = request.VaultName; //do not encrypt this. 
            Vault.AppliedCustomKey = request.UseUserKey;
            Vault.AddedBy = userId;
            Vault.AddedAt = DateTime.Now;
            Vault.Description = request.Description;
            Vault.IV = iv;
            Vault.StorageKeys = new List<VaultStorageKey>();
            request.Keys?.ForEach(rkey =>
            {
                var vsKey = new VaultStorageKey();
                vsKey.AddedAt = DateTime.Now;
                vsKey.AddedBy = userId;
                
                vsKey.EmailAddress = string.IsNullOrEmpty(rkey.Email) 
                                        ? rkey.Email 
                                        : _dataEncryptor.EncryptValue(key, iv, rkey.Email).Item1;
                
                vsKey.Password = _dataEncryptor.EncryptValue(key, iv, rkey.Password).Item1;
                vsKey.Username = string.IsNullOrEmpty(rkey.Username) 
                                    ? rkey.Username
                                    :_dataEncryptor.EncryptValue(key, iv, rkey.Username).Item1;
                vsKey.AccessLocation = string.IsNullOrEmpty(rkey.AccessLocation)
                                    ? rkey.AccessLocation
                                    : _dataEncryptor.EncryptValue(key, iv, rkey.AccessLocation).Item1;
                vsKey.IV = iv;

                vsKey.SecurityQAs = new List<VaultStorageKeySecurityQA>();
                rkey.SecurityQuestions?.ForEach(sq =>
                {
                    var vsq = new VaultStorageKeySecurityQA();
                    vsq.IV = iv;
                    vsq.Question = _dataEncryptor.EncryptValue(key,iv,sq.Question).Item1;
                    vsq.Answer = _dataEncryptor.EncryptValue(key,iv, sq.Answer).Item1;
                    vsq.AddedBy = userId;
                    vsq.AddedAt = DateTime.Now;
                    vsKey.SecurityQAs.Add(vsq);
                });

                Vault.StorageKeys.Add(vsKey);
            });

            var vaultRes = await _vaultRepository.AddAsync(Vault);
            
            res.IsSuccess = true;
            res.VaultId = vaultRes.VaultId;
            res.VaultName = vaultRes.VaultName;

            return res;
        }
    }
}
