using AccountManagement.Contract.Interfaces.Repositories;
using AccountManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Persistence.Implimentation.Repositories
{
    public class BlockWithdrawTransactionRepository : IBlockWithdrawTransactionRepository
    {
        private readonly AccountManagementDbContext _dbcontext;

        public BlockWithdrawTransactionRepository(AccountManagementDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Guid> Add(BlockWithdrawTransaction transaction)
        {
            await _dbcontext.BlockWithdrawTransactions.AddAsync(transaction);
            return transaction.Id;
        }
        public async Task<BlockWithdrawTransaction> FindByAccountId(Guid accountId)
        {
            var blockWithdrawTransaction = await _dbcontext.BlockWithdrawTransactions.FirstOrDefaultAsync(p => p.AccountId == accountId);
            return blockWithdrawTransaction;
        }
    }
}
