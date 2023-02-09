using AccountManagement.Contract.Dto;

namespace AccountManagement.Contract.Interfaces.Services
{
    public interface IBlockWithdrawTransactionService
    {
        Task<Guid> Add(AddBlockWithdrawTransactionRequest request);
        Task<GetWithdrawTransactionByAccountIdResponse> GetByAccountId(GetWithdrawTransactionByAccountIdRequest request);
    }
}
