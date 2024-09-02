using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Commands.AddSecurityQA
{
    public class AddSecurityQACommandValidator : AbstractValidator<AddSecurityQACommand>
    {
        public AddSecurityQACommandValidator()
        {
            RuleFor(x => x.Question)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Answer)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.KeyId)
                .NotNull()
                .NotEmpty();
        }
    }
}
