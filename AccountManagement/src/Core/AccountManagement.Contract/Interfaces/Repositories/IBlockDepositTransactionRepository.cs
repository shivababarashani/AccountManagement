using AccountManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Contract.Interfaces.Repositories
{
    public interface IBlockDepositTransactionRepository
    {
        Task<Guid> Add(BlockDepositTransaction transaction);
        Task<BlockDepositTransaction> FindByAccountId(Guid accountId);
    }
}
