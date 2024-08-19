using Microsoft.EntityFrameworkCore;
using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories.Contracts.Persistence.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories.Repositories.Users
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(PasSecDatabaseContext context) : base(context)
        {
        }

        public async Task<ApplicationUser?> GetUserAsync(Expression<Func<ApplicationUser, bool>>? filter)
        {
            if(filter == null)
                return await _context.ApplicationUsers.FirstOrDefaultAsync();

            return await _context.ApplicationUsers.Where(filter).FirstOrDefaultAsync();

        }

        public async Task<bool> IsUserNameTakenAsync(string userName)
        {
            var usernameExist = await _context.ApplicationUsers.AnyAsync(x => x.UserName!.Equals(userName));
            return usernameExist;
        }
    }
}
