using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator:AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator() {
            RuleFor(x=>x.UserName)
                .NotEmpty();
            RuleFor(x=>x.Password)
                .NotEmpty();
        }
    }
}
