using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories.Contracts.Persistence.Users;
using PasSecWebApi.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly string _securityKey;

        public LoginUserCommandHandler(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userRepository = userRepository;
            this.userManager = userManager;
            _securityKey = configuration["ApiSettings:Secret"]!;
        }

        async Task<LoginUserResponse> IRequestHandler<LoginUserCommand, LoginUserResponse>.Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var res = new LoginUserResponse();
            
            //validate
            var validator = new LoginUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(x=>x.ErrorMessage).ToList());

            //check user.
            var user = await _userRepository.GetUserAsync(x => x.UserName!.Equals(request.UserName)) ??
                throw new BadRequestException(["Invalid username or password."]);

            
            var validPassword= await userManager.CheckPasswordAsync(user, request.Password);

            if (!validPassword)
                throw new BadRequestException(["Invalid Password"]);

            //create token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_securityKey);
            var claims = new List<Claim> { new(ClaimTypes.Name, user.Id) };

            var secTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(secTokenDescriptor);
            res.Token = tokenHandler.WriteToken(token);
            res.UserName = request.UserName;
            res.IsSuccess = true;
            return res;
        }
    }
}
