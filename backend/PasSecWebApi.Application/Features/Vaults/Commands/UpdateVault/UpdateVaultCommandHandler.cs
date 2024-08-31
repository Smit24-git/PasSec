using MediatR;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using PasSecWebApi.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Vaults.Commands.UpdateVault
{
    public class UpdateVaultCommandHandler : IRequestHandler<UpdateVaultCommand, UpdateVaultCommandResponse>
    {
        private readonly IVaultRepository _vaultRepository;

        public UpdateVaultCommandHandler(IVaultRepository vaultRepository)
        {
            _vaultRepository = vaultRepository;
        }

        public async Task<UpdateVaultCommandResponse> Handle(UpdateVaultCommand request, CancellationToken cancellationToken)
        {
            var res = new UpdateVaultCommandResponse();

            var validator = new UpdateVaultCommandValidator();
            var validationRes = await validator.ValidateAsync(request, cancellationToken);
            if(!validationRes.IsValid)
                throw new ValidationException(validationRes.Errors.Select(e=>e.ErrorMessage).ToList());

            var vault = (await _vaultRepository.ListAllByFilterAsync(v=>v.VaultId == new Guid(request.VaultId))).FirstOrDefault()
                 ?? throw new BadRequestException(["Invalid Request"]);

            vault.VaultName = request.VaultName;
            vault.Description = request.Description;

            await _vaultRepository.UpdateAsync(vault);
            res.VaultId = vault.VaultId;
            res.VaultName = vault.VaultName;
            res.Description = vault.Description;
            res.IsSuccess = true;
            return res;
        }
    }
}
