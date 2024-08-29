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
    public class VaultStorageKeySecurityQARepository : BaseRepository<VaultStorageKeySecurityQA>, IVaultStorageKeySecurityQARepository
    {
        public VaultStorageKeySecurityQARepository(PasSecDatabaseContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<VaultStorageKeySecurityQA>> ListAllSequrityQAbyFilterAsync(Expression<Func<VaultStorageKeySecurityQA, bool>> filter)
        {
            return await _context.VaultStorageKeysSecurityQAs.Where(filter)
                .AsNoTracking().ToListAsync();
        }
    }
}
