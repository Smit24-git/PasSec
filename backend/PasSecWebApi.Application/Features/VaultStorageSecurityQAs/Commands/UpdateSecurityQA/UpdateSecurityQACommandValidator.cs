using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Commands.UpdateSecurityQA
{
    public class UpdateSecurityQACommandValidator : AbstractValidator<UpdateSecurityQACommand>
    {
        public UpdateSecurityQACommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Question)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Answer)
                .NotNull()
                .NotEmpty();
        }
    }
}
