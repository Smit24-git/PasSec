using FluentValidation;
using PasSecWebApi.Shared.regex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {

            RuleFor(x=>x.Password).NotEmpty()
                .MinimumLength(16)
                .Matches(PasswordFieldRegularExpression.PasswordFieldRegex())
                .WithMessage("Answer requirements not met.");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Invalid UserName");
        }
    }
}
