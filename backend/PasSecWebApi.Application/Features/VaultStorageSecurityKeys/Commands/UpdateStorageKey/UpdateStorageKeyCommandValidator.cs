using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Commands.UpdateStorageKey
{
    public class UpdateStorageKeyCommandValidator : AbstractValidator<UpdateStorageKeyCommand>
    {
        public UpdateStorageKeyCommandValidator()
        {
            RuleFor(e => e.VaultStorageKeyId)
                .NotNull()
                .NotEmpty();

            RuleFor(e => e.KeyName)
                .NotNull()
                .NotEmpty();
            
            RuleFor(e => e.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
