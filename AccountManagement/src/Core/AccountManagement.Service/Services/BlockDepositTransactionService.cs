using AccountManagement.Contract.Dto;
using AccountManagement.Contract.Interfaces.Repositories;
using AccountManagement.Contract.Interfaces.Services;
using AccountManagement.Domain.Entities;

namespace AccountManagement.Service.Services
{
    public class BlockDepositTransactionService: IBlockDepositTransactionService
    {
        private readonly IBlockDepositTransactionRepository _repository;
        public BlockDepositTransactionService(IBlockDepositTransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Add(AddBlockDepositTransactionRequest request)
        {
            var transaction = new BlockDepositTransaction
            {
                AccountId = request.AccountId,
                Status = request.Status,
                TraceNumber = request.TraceNumber,
                Description= request.Description,
                Date = request.Date,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            var transactionId = await _repository.Add(transaction);
            return transactionId;
        }
        public async Task<GetBlockTransactionByAccountIdResponse> GetByAccountId(GetBlockTransactionByAccountIdRequest request)
        {
            var transaction = await _repository.FindByAccountId(request.AccountId);
            return new GetBlockTransactionByAccountIdResponse
            {
                Date=transaction.Date,
                Description=transaction.Description,
                Status= transaction.Status,
                TraceNumber = transaction.TraceNumber   
            };
        }
    }
}
