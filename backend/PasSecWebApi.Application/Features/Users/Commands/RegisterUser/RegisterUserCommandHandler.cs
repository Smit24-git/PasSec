using MediatR;
using Microsoft.AspNetCore.Identity;
using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories.Contracts.Persistence.Users;
using PasSecWebApi.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<ApplicationUser> userManager;
        public RegisterUserCommandHandler(IUserRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            this.userManager = userManager;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var res = new RegisterUserResponse();
            var validation = new RegisterUserCommandValidator();
            var validationRes = await validation.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            
            // validate request object
            if (!validationRes.IsValid)
            {
                res.Errors = validationRes.Errors.Select(x=>x.ErrorMessage).ToList();
                res.IsSuccess = false;
                throw new ValidationException(res.Errors);
            }

            //validate with database
            var usernameAlreadyExists = await _repository.IsUserNameTakenAsync(request.UserName);
            if(usernameAlreadyExists)
                throw new ValidationException(["Username Already Exists"]);

            // register user
            var result = await userManager.CreateAsync(new ApplicationUser
            {
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpper(),
            }, request.Password);

            if(!result.Succeeded)
                throw new ValidationException(result.Errors.Select(e => e.Description).ToList());
              
            res.UserName = request.UserName;
            res.IsSuccess = true;
            return res;
        }
    }
}
