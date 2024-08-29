using PasSecWebApi.Persistence;
using System.Linq.Expressions;

namespace PasSecWebApi.Repositories.Contracts.Persistence.Vaults
{
    public interface IVaultStorageKeyRepository : IAsyncRepository<VaultStorageKey>
    {
        Task<IReadOnlyList<VaultStorageKey>> ListAllStorageKeysbyFilterAsync(Expression<Func<VaultStorageKey, bool>> filter);

    }
}
