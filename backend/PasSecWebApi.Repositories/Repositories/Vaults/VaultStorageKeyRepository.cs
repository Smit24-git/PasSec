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
    internal class VaultStorageKeyRepository : BaseRepository<VaultStorageKey>, IVaultStorageKeyRepository
    {
        public VaultStorageKeyRepository(PasSecDatabaseContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<VaultStorageKey>> ListAllStorageKeysbyFilterAsync(Expression<Func<VaultStorageKey, bool>> filter)
        {
            return await _context.VaultStorageKeys.Where(filter).AsNoTracking().ToListAsync();
        }
    }
}
