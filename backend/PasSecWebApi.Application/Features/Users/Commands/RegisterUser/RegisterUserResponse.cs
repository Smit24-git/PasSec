using PasSecWebApi.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserResponse:BaseResponse
    {
        public string UserName { get; set; } = string.Empty;
    }
}
