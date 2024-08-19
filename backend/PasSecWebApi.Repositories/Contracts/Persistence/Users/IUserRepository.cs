using PasSecWebApi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories.Contracts.Persistence.Users
{
    public interface IUserRepository:IAsyncRepository<ApplicationUser>
    {
        Task<bool> IsUserNameTakenAsync(string userName);
        Task<ApplicationUser?> GetUserAsync(Expression<Func<ApplicationUser,bool>>? filter);
    }
}
