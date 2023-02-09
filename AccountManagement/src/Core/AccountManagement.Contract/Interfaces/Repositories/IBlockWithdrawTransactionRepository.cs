using AccountManagement.Domain.Entities;

namespace AccountManagement.Contract.Interfaces.Repositories
{
    public interface IBlockWithdrawTransactionRepository
    {
        Task<Guid> Add(BlockWithdrawTransaction transaction);
        Task<BlockWithdrawTransaction> FindByAccountId(Guid accountId);
    }
}
