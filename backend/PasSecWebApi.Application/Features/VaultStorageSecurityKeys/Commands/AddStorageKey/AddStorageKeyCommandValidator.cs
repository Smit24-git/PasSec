using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Commands.AddStorageKey
{
    public class AddStorageKeyCommandValidator : AbstractValidator<AddStorageKeyCommand>
    {
        public AddStorageKeyCommandValidator()
        {
            RuleFor(e => e.VaultId)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.KeyName)
                .NotNull()
                .NotEmpty();
            RuleFor(e=>e.Password)
                .NotNull()
                .NotEmpty();
            RuleForEach(e => e.SecurityQAs)
                .SetValidator(new AddStorageQAValidator());
        }
    }

    public class AddStorageQAValidator: AbstractValidator<AddStorageKeyQA>
    {
        public AddStorageQAValidator()
        {
            RuleFor(e => e.Question)
                .NotNull()
                .NotEmpty();

            RuleFor(e => e.Answer)
                .NotNull()
                .NotEmpty();
        }
    }
}
