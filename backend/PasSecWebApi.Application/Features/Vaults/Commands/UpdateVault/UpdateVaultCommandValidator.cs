using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Vaults.Commands.UpdateVault
{
    public class UpdateVaultCommandValidator : AbstractValidator<UpdateVaultCommand>
    {
        public UpdateVaultCommandValidator()
        {
            RuleFor(e => e.VaultId)
                .NotNull()
                .NotEmpty();

            RuleFor(e => e.VaultName)
                .NotNull()
                .NotEmpty();
        }

    }
}
