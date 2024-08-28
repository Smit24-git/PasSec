using PasSecWebApi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories.Contracts.Persistence.Vaults
{
    public interface IVaultRepository:IAsyncRepository<Vault>
    {
        Task<IReadOnlyList<Vault>> ListAllByFilterAsync(Expression<Func<Vault, bool>> filter);
    }
}
