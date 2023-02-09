using AccountManagement.Contract.Interfaces.Repositories;
using AccountManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Persistence.Implimentation.Repositories
{
    public class BlockDepositTransactionRepository: IBlockDepositTransactionRepository
    {
        private readonly AccountManagementDbContext _dbcontext;

        public BlockDepositTransactionRepository(AccountManagementDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Guid> Add(BlockDepositTransaction transaction)
        {
            await _dbcontext.BlockDepositTransactions.AddAsync(transaction);
            return transaction.Id;
        }
        public async Task<BlockDepositTransaction> FindByAccountId(Guid accountId)
        {
            var blockDepositTransaction = await _dbcontext.BlockDepositTransactions.FirstOrDefaultAsync(p => p.AccountId == accountId);
            return blockDepositTransaction;
        }
    }
}
