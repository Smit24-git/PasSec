using Microsoft.EntityFrameworkCore;
using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories.Repositories.Vaults
{
    public class VaultRepository : BaseRepository<Vault>, IVaultRepository
    {
        public VaultRepository(PasSecDatabaseContext context) : base(context)
        {

        }

        public async Task<IReadOnlyList<Vault>> ListAllByFilterAsync(Expression<Func<Vault, bool>> filter)
        {
            return await _context.Vaults.Where(filter)
                .AsNoTracking()
                .ToListAsync();  

        }
    }
}
