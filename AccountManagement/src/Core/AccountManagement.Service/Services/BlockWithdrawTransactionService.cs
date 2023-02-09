using AccountManagement.Contract.Dto;
using AccountManagement.Contract.Interfaces.Repositories;
using AccountManagement.Contract.Interfaces.Services;
using AccountManagement.Domain.Entities;

namespace AccountManagement.Service.Services
{
    public class BlockWithdrawTransactionService : IBlockWithdrawTransactionService
    {
        private readonly IBlockWithdrawTransactionRepository _repository;
        public BlockWithdrawTransactionService(IBlockWithdrawTransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Add(AddBlockWithdrawTransactionRequest request)
        {
            var transaction = new BlockWithdrawTransaction
            {
                AccountId= request.AccountId,
                Status = request.Status,
                TraceNumber = request.TraceNumber,
                Description = request.Description,
                Date = request.Date,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            var transactionId = await _repository.Add(transaction);
            return transactionId;
        }
        public async Task<GetWithdrawTransactionByAccountIdResponse> GetByAccountId(GetWithdrawTransactionByAccountIdRequest request)
        {
            var transaction = await _repository.FindByAccountId(request.AccountId);
            return new GetWithdrawTransactionByAccountIdResponse
            {
                Date = transaction.Date,
                Description = transaction.Description,
                Status = transaction.Status,
                TraceNumber = transaction.TraceNumber
            };
        }
    }
}
