using AccountManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Contract.Interfaces.Services
{
    public interface IBlockDepositTransactionService
    {
        Task<Guid> Add(AddBlockDepositTransactionRequest request);
        Task<GetBlockTransactionByAccountIdResponse> GetByAccountId(GetBlockTransactionByAccountIdRequest request);
    }
}
