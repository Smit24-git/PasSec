using PasSecWebApi.Persistence;
using System.Linq.Expressions;

namespace PasSecWebApi.Repositories.Contracts.Persistence.Vaults
{
    public interface IVaultStorageKeySecurityQARepository : IAsyncRepository<VaultStorageKeySecurityQA>
    {
        Task<IReadOnlyList<VaultStorageKeySecurityQA>> ListAllSequrityQAbyFilterAsync(Expression<Func<VaultStorageKeySecurityQA, bool>> filter);
    }
}
