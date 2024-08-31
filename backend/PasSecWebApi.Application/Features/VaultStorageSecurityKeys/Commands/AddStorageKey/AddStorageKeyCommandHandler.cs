using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories.Contracts.Infrastructure;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using PasSecWebApi.Repositories.Infrastructure;
using PasSecWebApi.Shared.Dtos;
using PasSecWebApi.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Commands.AddStorageKey
{
    internal class AddStorageKeyCommandHandler : IRequestHandler<AddStorageKeyCommand, AddStorageKeyCommandResponse>
    {
        private readonly string _key;
        private readonly IMapper _mapper;
        private readonly IVaultRepository _vaultRepository;
        private readonly IVaultStorageKeyRepository _storageKeyRepository;
        private readonly IDataEncryptor _encryptor;
        private readonly IHttpContextAccessor _contextAccessor;

        public AddStorageKeyCommandHandler( 
            IConfiguration configuration, 
            IMapper mapper, 
            IVaultRepository vaultRepository, 
            IVaultStorageKeyRepository storageKeyRepository, 
            IDataEncryptor encryptor,
            IHttpContextAccessor httpContextAccessor)
        {
            _key = configuration["ApiSettings:secret"]!;
            _mapper = mapper;
            _vaultRepository = vaultRepository;
            _storageKeyRepository = storageKeyRepository;
            _encryptor = encryptor;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<AddStorageKeyCommandResponse> Handle(AddStorageKeyCommand request, CancellationToken cancellationToken)
        {
            var res = new AddStorageKeyCommandResponse();
            string userId = _contextAccessor.HttpContext.User.Identity!.Name!;

            //validation
            var validator = new AddStorageKeyCommandValidator();
            var validationRes = await validator.ValidateAsync(request, cancellationToken);
            if(!validationRes.IsValid)
                throw new ValidationException(validationRes.Errors.Select(x=>x.ErrorMessage).ToList());

            
            var vaultId = new Guid(request.VaultId);
            var vault = (await _vaultRepository.ListAllByFilterAsync(x=>x.VaultId == vaultId)).FirstOrDefault()
                ?? throw new BadRequestException(["Invalid Request"]);

            string securityKey = _key;
            var IV = vault.IV;

            //update security key 
            if (vault.AppliedCustomKey)
            {
                if(string.IsNullOrEmpty(request.UserKey))
                {
                    throw new ValidationException(["User Key is Required"]);
                }
                securityKey = request.UserKey;
            }


            var vsKey = new VaultStorageKey();
            vsKey.VaultId = vaultId;
            vsKey.KeyName = request.KeyName;
            vsKey.AddedAt = DateTime.Now;
            vsKey.AddedBy = userId;
            vsKey.EmailAddress = string.IsNullOrEmpty(request.Email)
            ? request.Email
            : _encryptor.EncryptValue(securityKey, IV, request.Email).Item1;

            vsKey.Password = _encryptor.EncryptValue(securityKey, IV, request.Password).Item1;
            vsKey.Username = string.IsNullOrEmpty(request.Username)
                ? request.Username
                : _encryptor.EncryptValue(securityKey, IV, request.Username).Item1;
            vsKey.AccessLocation = string.IsNullOrEmpty(request.AccessLocation)
            ? request.AccessLocation
            : _encryptor.EncryptValue(securityKey, IV, request.AccessLocation).Item1;
            vsKey.IV = IV;

            vsKey.SecurityQAs = new List<VaultStorageKeySecurityQA>();
            request.SecurityQAs?.ForEach(sq =>
            {
                var vsq = new VaultStorageKeySecurityQA();
                vsq.IV = IV;
                vsq.Question = _encryptor.EncryptValue(securityKey, IV, sq.Question).Item1;
                vsq.Answer = _encryptor.EncryptValue(securityKey, IV, sq.Answer).Item1;
                vsq.AddedBy = userId;
                vsq.AddedAt = DateTime.Now;
                vsKey.SecurityQAs.Add(vsq);
            });

            await _storageKeyRepository.AddAsync(vsKey);
            res.IsSuccess = true;
            return res;

        }
    }
}
