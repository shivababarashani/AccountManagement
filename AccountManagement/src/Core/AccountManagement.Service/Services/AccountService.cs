using AccountManagement.Contract.Dto;
using AccountManagement.Contract.Interfaces.Proxy;
using AccountManagement.Contract.Interfaces.Repositories;
using AccountManagement.Contract.Interfaces.Services;
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Enums;
using AccountManagement.Framework.ApiResult;
using AccountManagement.Framework.Common;
using AccountManagement.Framework.Exceptions;
using AccountManagement.Framework.Logger;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IXProxy _dotinProxy;
        private readonly ILogCoreFactory _logCore;
        public AccountService(IAccountRepository accountRepository,
                              IXProxy dotinProxy,
                              ILogCoreFactory logCore)
        {
            _accountRepository = accountRepository;
            _dotinProxy = dotinProxy;
            _logCore = logCore;
        }
        #region Public Methods
        public async Task<Guid> Add(AddAccountRequest request)
        {
            var account = new Account
            {
                Number = request.Number,
                Iban = request.Iban,
                AvailableAmount = request.AvailableAmount,
                ActualAmount = request.ActualAmount,
                DemandPacketId = request.DemandPacketId,
                TypeDescription = request.TypeDescription,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            var accountId = await _accountRepository.InsertAsync(account);
            return accountId;
        }
        public async Task<GetAccountOwnershipTypeResponse> GetAccountOwnershipTypes()
        {
            var accountTypes = await _accountRepository.AccountOwnershipTypeAsync();
            List<AccountOwnershipTypeItem> AccountOwnershipTypeItems = new List<AccountOwnershipTypeItem>();
            GetAccountOwnershipTypeResponse response = new GetAccountOwnershipTypeResponse();

            foreach (var item in accountTypes)
            {
                AccountOwnershipTypeItems.Add(new AccountOwnershipTypeItem { Code = item.Id, Title = item.Title });
            }
            response.AccountOwnershipTypeItems = AccountOwnershipTypeItems;

            return response;
        }


        public async Task<GetSubscriptionTypesResponse> GetSubscriptionTypes()
        {
            var subscriptionData = await _accountRepository.GetSubscriptionTypeAsync();
            List<SubscriptionTypeItem> SubscriptionTypeItems = new List<SubscriptionTypeItem>();
            GetSubscriptionTypesResponse answer = new GetSubscriptionTypesResponse();

            foreach (var item in subscriptionData)
            {
                SubscriptionTypeItems.Add(new SubscriptionTypeItem { Code = item.Id, Title = item.Title });
            }
            answer.SubscriptionTypeItems = SubscriptionTypeItems;
            return answer;
        }


        public async Task<GetAccountByDemandPacketIdResponse> GetAccountsByDemandPacketId(GetAccountByDemandPacketIdRequest request)
        {
            var accounts = await _accountRepository.GetAccountsByDemandPacketId(request.DemandPacketId);

            var result = new GetAccountByDemandPacketIdResponse();
            foreach (var account in accounts)
            {
                result.Accounts.Add(new AccountDetail
                {
                    Id = account.Id,
                    Number = account.Number,
                    Iban = account.Iban,
                    ActualAmount = account.ActualAmount,
                    AvailableAmount = account.AvailableAmount,
                });
            };
            return result;
        }
        public async Task<ActionResult<GetCustomerAccountsResponse>> GetCustomerAccounts(GetCustomerAccountsRequest request)
        {
            var customerAccounts = await GetCustomerAccountsProxy(request);
            return GenerateResponse(customerAccounts);
        }
        public async Task<ActionResult<GetAccountInfoResponse>> GetAccountInfo(GetAccountInfoRequest request)
        {
            var accountInfo = await GetAccountInfoProxy(request);
            return GenerateResponse(accountInfo);
        }

        public async Task<GetSwiftTypesResponse> GetSwiftTypes()
        {
            var swiftTypes = await _accountRepository.GetSwiftTypesAsync();
            List<SwiftTypeItem> swiftTypeItems = new List<SwiftTypeItem>();
            GetSwiftTypesResponse result = new GetSwiftTypesResponse();

            foreach (var swiftType in swiftTypes)
            {
                swiftTypeItems.Add(new SwiftTypeItem { Code = swiftType.Id, Title = swiftType.Title });
            }
            result.SwiftTypeItems = swiftTypeItems;
            return result;
        }
        #endregion

        #region Private Methods
        private async Task<ActionResult<GetAccountInfoProxyResponse>> GetAccountInfoProxy(GetAccountInfoRequest request)
        {
            var requset = new GetAccountInfoProxyRequest
            {
                DepositNumberOrIBAN = request.AccoutnNumberOrIBAN
            };
            var accountInfo = await _dotinProxy.GetAccountInfo(requset);

            //ToDo: اینجا حتما خروجی چک بشه که اگر خروجی موفق نبود تو یه جدول ذخیره بشه
            MapMessageAndStatusCode(accountInfo);

            return accountInfo;
        }

        private void MapMessageAndStatusCode(ActionResult<GetCustomerAccountsProxyResponse> customerInfo)
        {
            if (!customerInfo.IsSuccess)
            {
                switch (customerInfo.StatusCode)
                {
                    case 1038:
                        customerInfo.StatusCode = (int)DemandStatusEnum.InputIncorrect;
                        customerInfo.Message = "اطلاعات ورودی اشتباه است.";
                        break;
                    case 0:
                        customerInfo.StatusCode = (int)DemandStatusEnum.CustomerNumberInValid;
                        customerInfo.Message = "شماره مشتری معتبر نیست.";
                        break;
                    case 1676:
                        customerInfo.StatusCode = (int)DemandStatusEnum.NotExistAccount;
                        customerInfo.Message = "برای کدملی وارد شده مشتری فعالی یافت نشد.";
                        break;
                    case 2941:
                        customerInfo.StatusCode = (int)DemandStatusEnum.NoFillMandatoryInput;
                        customerInfo.Message = "مقداری برای پارامتر ورودی اجباری ارسال نشده است .";
                        break;
                    default:
                        customerInfo.StatusCode = (int)DemandStatusEnum.NotAvailable;
                        customerInfo.Message = "سرویس در دسترس نمی باشد";
                        break;
                }
            }

        }
        private void MapMessageAndStatusCode(ActionResult<GetAccountInfoProxyResponse> accountInfo)
        {
            if (!accountInfo.IsSuccess)
            {
                switch (accountInfo.StatusCode)
                {
                    case 1038:
                        accountInfo.StatusCode = (int)DemandStatusEnum.InputIncorrect;
                        accountInfo.Message = "اطلاعات ورودی اشتباه است.";
                        break;
                    case 2941:
                        accountInfo.StatusCode = (int)DemandStatusEnum.NoFillMandatoryInput;
                        accountInfo.Message = "مقداری برای پارامتر ورودی اجباری ارسال نشده است .";
                        break;
                    default:
                        accountInfo.StatusCode = (int)DemandStatusEnum.NotAvailable;
                        accountInfo.Message = "سرویس در دسترس نمی باشد";
                        break;
                }
            }

        }

        private ActionResult<GetCustomerAccountsResponse> GenerateResponse(ActionResult<GetCustomerAccountsProxyResponse> customerAccounts)
        {
            var response = new ActionResult<GetCustomerAccountsResponse>(customerAccounts.IsSuccess, customerAccounts.StatusCode, null, customerAccounts.Message);
            var accounts = new List<CustomerAccount>();
            response.Data = new GetCustomerAccountsResponse();
            if (customerAccounts.Data.Accounts != null)
            {
                foreach (var account in customerAccounts.Data.Accounts)
                {
                    accounts.Add(new CustomerAccount
                    {
                        AccountNumber = account.AccountNumber,
                        AccountIban = account.AccountIban,
                        AccountState = account.AccountState,
                        AccountTypeTitle = account.AccountTypeTitle,
                        CreatorBranchCode = account.CreatorBranchCode,
                        OpeningDate = account.OpeningDate,
                        WithdrawRight = account.WithdrawRight,
                        SubscriptionType = account.SubscriptionType,
                    });
                }
                response.Data.Accounts = accounts;
            }
            return response;
        }
        private ActionResult<GetAccountInfoResponse> GenerateResponse(ActionResult<GetAccountInfoProxyResponse> accountInfo)
        {
            var customers = new List<CustomerInfo>();
            var response = new ActionResult<GetAccountInfoResponse>(accountInfo.IsSuccess, accountInfo.StatusCode, null, accountInfo.Message);
            response.Data = new GetAccountInfoResponse();

            response.Data.ActualAmount = accountInfo.Data.ActualAmount;
            response.Data.AvailableAmount = accountInfo.Data.AvailableAmount;
            response.Data.BackupAccountNumber = accountInfo.Data.BackupAccountNumber;
            response.Data.CreatorBranchCode = accountInfo.Data.CreatorBranchCode;
            response.Data.Iban = accountInfo.Data.Iban;
            response.Data.Number = accountInfo.Data.Number;
            response.Data.OwnershipTypeId = accountInfo.Data.OwnershipTypeId;
            response.Data.TypeDescription = accountInfo.Data.TypeDescription;
            response.Data.CreateDate = accountInfo.Data.CreateDate;
            response.Data.SharePercent = accountInfo.Data.SharePercent;
            response.Data.Status = accountInfo.Data.Status;

            if (accountInfo.Data.CustomerInfoProxys != null)
            {
                foreach (var customer in accountInfo.Data.CustomerInfoProxys)
                {
                    var customerInfo = new CustomerInfo
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        NationalCode = customer.NationalCode,
                        CustomerType = customer.CustomerType
                    };
                    customers.Add(customerInfo);
                }
                response.Data.CustomerInfos = customers;
            }
            return response;
        }
        private async Task<ActionResult<GetCustomerAccountsProxyResponse>> GetCustomerAccountsProxy(GetCustomerAccountsRequest request)
        {
            var customerAccountRequest = new GetCustomerAccountsProxyRequest
            {
                NationalCode = request.NationalCode,
            };
            var customerAccounts = await _dotinProxy.GetCustomerAccounts(customerAccountRequest);

            //ToDo: اینجا حتما خروجی چک بشه که اگر خروجی موفق نبود تو یه جدول ذخیره بشه
            MapMessageAndStatusCode(customerAccounts);
            return customerAccounts;
        }
        #endregion

    }
}
