using AccountManagement.Contract.Dto;
using AccountManagement.Contract.Enums;
using AccountManagement.Contract.Interfaces.Repositories;
using AccountManagement.Contract.Interfaces.Services;
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Enums;
using AccountManagement.Framework.ApiResult;
using AccountManagement.Framework.QueueProducer;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Service.Services
{
    public class DemandPacketService : IDemandPacketService
    {
        private readonly IDemandPacketRepository _demandPacketRepository;
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly ILetterService _letterService;
        private readonly IBlockDepositTransactionService _blockDepositTransactionService;
        private readonly IBlockWithdrawTransactionService _blockWithdrawTransactionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueueProducer _queueProducer;

        public DemandPacketService(IDemandPacketRepository demandPacketRepository,
                                   ICustomerService customerService,
                                   IAccountService accountService,
                                   ILetterService letterService,
                                   IBlockDepositTransactionService blockDepositTransactionService,
                                   IBlockWithdrawTransactionService blockWithdrawTransactionService,
                                   IUnitOfWork unitOfWork,
                                   IQueueProducer queueProducer)
        {
            _demandPacketRepository = demandPacketRepository;
            _customerService = customerService;
            _accountService = accountService;
            _letterService = letterService;
            _blockDepositTransactionService = blockDepositTransactionService;
            _blockWithdrawTransactionService = blockWithdrawTransactionService;
            _unitOfWork = unitOfWork;
            _queueProducer = queueProducer;
        }
        #region Public Methods
        public async Task<ExecutDemandPacketStatus> ExecutDemandPacket(ExecutDemandPacketRequest request)
        {
            AddDemandPacketRequest addDemandPacketRequest = GenerateAddDemandRequest(request);
            var demandPacketId = await Add(addDemandPacketRequest);
            await SaveChanges();
            switch (request.LetterType)
            {
                case LetterTypeEnum.NationalCode:
                    return await DemandByNationalCode(request, demandPacketId);
            }
            return ExecutDemandPacketStatus.Unsuccessful;

        }

        public async Task<Guid> Add(AddDemandPacketRequest request)
        {
            var demandPacketAccountOwnershipTypes = new List<DemandPacketAccountOwnershipType>();
            var demandPacketSubscriptionTypes = new List<DemandPacketSubscriptionType>();
            var demandPacket = new DemandPacket
            {
                LetterId = request.LetterId,
                BlockUnblockReasonId = (int)request.BlockUnblockReason,
                SwiftTypeId = (int)request.SwiftType,
                DemandStatusId = (int)request.DemandStatus,
                TraceCode = request.TraceCode,
                Value = request.Value,
                BlockUnblockPassword = request.BlockUnblockPassword,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            foreach (var accountOwnershipType in request.AccountOwnershipTypes)
            {
                var demandPacketAccountOwnershipType = new DemandPacketAccountOwnershipType
                {
                    DemandPacket = demandPacket,
                    AccountOwnershipTypeId = (int)accountOwnershipType
                };
                demandPacketAccountOwnershipTypes.Add(demandPacketAccountOwnershipType);
            }
            foreach (var subscriptionType in request.SubscriptionTypes)
            {
                var demandPacketSubscriptionType = new DemandPacketSubscriptionType
                {
                    DemandPacket = demandPacket,
                    SubscriptionTypeId = (int)subscriptionType
                };
                demandPacketSubscriptionTypes.Add(demandPacketSubscriptionType);
            }
            demandPacket.DemandPacketAccountOwnershipTypes = demandPacketAccountOwnershipTypes;
            demandPacket.DemandPacketSubscriptionTypes = demandPacketSubscriptionTypes;
            var demandPacketId = await _demandPacketRepository.InsertAsync(demandPacket);
            return demandPacketId;
        }
        public async Task<DemandPacket> FindById(Guid id)
        {
            return await _demandPacketRepository.FindById(id);
        }
        public async Task SetDemandPacketStatus(SetDemandPacketStatusRequest request)
        {
            var demandPacket = await FindById(request.Id);
            demandPacket.DemandStatusId = (int)request.DemandStatusId;
            demandPacket.ModifiedDate = DateTime.Now;
            _demandPacketRepository.SetDemandPacketStatus(demandPacket);
        }
        #endregion

        #region Private Methods
        private async Task<ExecutDemandPacketStatus> DemandByNationalCode(ExecutDemandPacketRequest request, Guid demandPacketId)
        {
            var customerAccounts = await GetCustomerAccounts(request.Value);
            if (customerAccounts.StatusCode == 0)
            {
                if (IsMatchSubscriptionType(customerAccounts.Data.Accounts, request.SubscriptionTypes))
                {
                    var accountsForBlock = customerAccounts.Data.Accounts.Where(l => request.SubscriptionTypes.Contains(l.SubscriptionType)).ToList();
                    foreach (var customerAccount in accountsForBlock)
                    {
                        //var accountInfo = await GetAccountDetail(customerAccount.AccountNumber);
                        var accountInfo = await GetAccountDetail("3117112179449");
                        if (accountInfo.StatusCode == 0)
                        {
                            if (IsMatchAccountOwnershipType(accountInfo.Data.OwnershipTypeId, request.AccountOwnershipTypes))
                            {
                                var accountId = await AddAccount(accountInfo.Data, demandPacketId);
                                //var customerInfo = await GetCustomerInfo(request.Value);
                                foreach (var customerInfo in accountInfo.Data.CustomerInfos)
                                {
                                    await AddCustomer(accountInfo.Data.CustomerInfos, accountId);
                                }
                                await SaveChanges();
                                var queueRequest = await GenerateBlockQueuesRequest(demandPacketId, accountId, accountInfo.Data, request);
                                _queueProducer.SendMessage("BlockWithdraw", queueRequest);
                                _queueProducer.SendMessage("BlockDeposit", queueRequest);
                            }
                            else
                            {
                                SetDemandStatusNoMatchedAccount(demandPacketId);
                            }
                        }
                        else
                        {
                            var SetDemandPacketStatusrequest = new SetDemandPacketStatusRequest()
                            {
                                DemandStatusId = (DemandStatusEnum)accountInfo.StatusCode,
                                Id = demandPacketId
                            };
                            await SetDemandPacketStatus(SetDemandPacketStatusrequest);
                        }
                    }
                }
                else
                {
                    SetDemandStatusNoMatchedAccount(demandPacketId);
                }
            }
            else
            {
                var SetDemandPacketStatusrequest = new SetDemandPacketStatusRequest()
                {
                    DemandStatusId = (DemandStatusEnum)customerAccounts.StatusCode,
                    Id = demandPacketId
                };
                await SetDemandPacketStatus(SetDemandPacketStatusrequest);
            }
            await SaveChanges();
            return GenerateResponse();
        }

        private ExecutDemandPacketStatus GenerateResponse()
        {
            return ExecutDemandPacketStatus.Successful;
        }

        private bool IsMatchAccountOwnershipType(CustomerType ownershipTypeId, List<AccountOwnershipTypeEnum> accountOwnershipTypes)
        {
            AccountOwnershipTypeEnum ownershipType = MapOwnershipType(ownershipTypeId);
            if (accountOwnershipTypes.Contains(ownershipType))
            {
                return true;
            }
            return false;
        }

        private AccountOwnershipTypeEnum MapOwnershipType(CustomerType ownershipTypeId)
        {
            switch (ownershipTypeId)
            {
                case CustomerType.RealCustomer:
                    return AccountOwnershipTypeEnum.RealCustomer;
                case CustomerType.LegalCustomer:
                    return AccountOwnershipTypeEnum.LegalCustomer;
                case CustomerType.ForeignRealCustomer:
                    return AccountOwnershipTypeEnum.RealCustomer;
                case CustomerType.ForeignLegalCustomer:
                    return AccountOwnershipTypeEnum.LegalCustomer;
                default:
                    return AccountOwnershipTypeEnum.RealCustomer;
            }
        }

        private bool IsMatchSubscriptionType(List<CustomerAccount> accounts, List<SubscriptionTypeEnum> subscriptionTypes)
        {
            if (accounts.Any(x => subscriptionTypes.Any(y => y == x.SubscriptionType)))
            {
                return true;
            }
            return false;
        }

        private AddDemandPacketRequest GenerateAddDemandRequest(ExecutDemandPacketRequest request)
        {
            var demandPacketRequest = new AddDemandPacketRequest
            {
                BlockUnblockReason = request.BlockUnblockReason,
                AccountOwnershipTypes = request.AccountOwnershipTypes,
                TraceCode = request.TraceCode,
                LetterId = request.LetterId,
                Value = request.Value,
                BlockUnblockPassword = request.BlockUnblockPassword,
                SwiftType = request.SwiftType,
                DemandStatus = DemandStatusEnum.DoingTransaction,
                SubscriptionTypes = request.SubscriptionTypes,
            };
            return demandPacketRequest;
        }
        private async Task<ActionResult<GetAccountInfoResponse>> GetAccountDetail(string accountNumber)
        {
            var request = new GetAccountInfoRequest
            {
                AccoutnNumberOrIBAN = accountNumber
            };
            return await _accountService.GetAccountInfo(request);
        }
        private async Task<ActionResult<GetCustomerAccountsResponse>> GetCustomerAccounts(string nationalCode)
        {
            var request = new GetCustomerAccountsRequest
            {
                NationalCode = nationalCode
            };
            var customerAccounts = await _accountService.GetCustomerAccounts(request);
            return customerAccounts;
        }
        private async Task<Guid> AddAccount(GetAccountInfoResponse accountInfo, Guid demandPacketId)
        {
            var accountRequest = new AddAccountRequest
            {
                Number = accountInfo.Number,
                Iban = accountInfo.Iban,
                AvailableAmount = accountInfo.AvailableAmount,
                ActualAmount = accountInfo.ActualAmount,
                DemandPacketId = demandPacketId,
                TypeDescription = accountInfo.TypeDescription,
            };
            return await _accountService.Add(accountRequest);
        }

        private async Task AddCustomer(List<CustomerInfo> customerInfos, Guid accountId)
        {
            foreach (var customerInfo in customerInfos)
            {
                var customerRequest = new AddCustomerRequest
                {
                    FirstName = customerInfo.FirstName,
                    LastName = customerInfo.LastName,
                    NationalCode = customerInfo.NationalCode,
                    AccountId = accountId
                };
                await _customerService.Add(customerRequest);
            }

        }
        private async void SetDemandStatusNoMatchedAccount(Guid demandPacketId)
        {
            var request = new SetDemandPacketStatusRequest()
            {
                DemandStatusId = DemandStatusEnum.NoMatchedAccount,
                Id = demandPacketId
            };
            await SetDemandPacketStatus(request);
        }

        private async Task SaveChanges()
        {
            await _unitOfWork.SaveChangesAsync();
        }
        private async Task<BlockQueueRequest> GenerateBlockQueuesRequest(Guid demandPacketId, Guid accountId, GetAccountInfoResponse accountInfo, ExecutDemandPacketRequest request)
        {
            var letter = await GetLetterById(request.LetterId);
            var queueRequest = new BlockQueueRequest
            {
                AccountId = accountId,
                DemandPacketId = demandPacketId,
                AccountNumber = accountInfo.Number,
                BlockReason = request.BlockUnblockReason,
                LetterContext = letter.Content,
                LetterDate = letter.Date,
                LetterDeadline = letter.Deadline,
                LetterNumber = letter.Number,
                LetterTitle = letter.Title,
                ReceiptLetterDate = letter.ReceiptDate,
                BlockUnblockPassword = request.BlockUnblockPassword,
                SwiftType = request.SwiftType,
                LetterContextImage = letter.ContextImage
            };
            return queueRequest;
        }
        private async Task<FindLetterByIdResponse> GetLetterById(Guid letterId)
        {
            var request = new FindLetterByIdRequest
            {
                LetterId = letterId
            };
            return await _letterService.FindLetterById(request);
        }


        #endregion
    }
}
