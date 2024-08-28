using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories.Repositories.Vaults
{
    public class VaultRepository : BaseRepository<Vault>, IVaultRepository
    {
        public VaultRepository(PasSecDatabaseContext context) : base(context)
        {

        }
    }
}
