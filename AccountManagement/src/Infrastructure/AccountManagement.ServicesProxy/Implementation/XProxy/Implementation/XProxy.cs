using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using AccountManagement.Contract.Dto;
using AccountManagement.Contract.Dto.Setting;
using AccountManagement.Contract.Interfaces.Proxy;
using AccountManagement.Framework.ApiResult;
using AccountManagement.ServicesProxy.Implementation.XProxy.Consts;
using AccountManagement.ServicesProxy.Implementation.XProxy.Dto;
using AccountManagement.Contract.Enums;
using AccountManagement.Domain.Enums;
using AccountManagement.Framework.Logger;
using AccountManagement.Framework.Common;
using Microsoft.Extensions.Logging;
using System.Security.Authentication;

namespace AccountManagement.ServicesProxy.Implementation.XProxy.Implementation
{
    public class XProxy : IXProxy
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISignature _signature;
        private readonly SiteSetting _siteSetting;
        private readonly ILogCoreFactory _logCore;
        public XProxy(ISignature signature,
                          IHttpClientFactory clientFactory,
                          IOptions<SiteSetting> options,
                          ILogCoreFactory logCore)
        {
            _clientFactory = clientFactory;
            _signature = signature;
            _siteSetting = options.Value;
            _logCore = logCore;
        }
        public async Task<ActionResult<GetCustomerInfoResponse>> GetCustomerInfo(GetCustomerInfoRequest request)
        {
            try
            {
                var startTime = DateTime.Now;
                var proxyResp = await Post(request,XApiAddresses.GetCustomerInfo, "7");
                var responseString = proxyResp.Content.ReadAsStringAsync().Result;

                var responseTime = Convert.ToInt32((DateTime.Now - startTime).TotalMilliseconds);
                _logCore.ActionLog(TransactionType.GetCustomerInfo, "GetCustomerInfo", (int)proxyResp.StatusCode, responseTime, JsonConvert.SerializeObject(request), responseString);

                if (proxyResp.StatusCode == HttpStatusCode.OK)
                {
                    var response = JsonConvert.DeserializeObject<GetCustomerInfoProxyResponse>(responseString);
                    if (response.RsCode == (int)BlockDepositProxyStatus.Sucessful)
                    {
                        return GenerateResponse(response);
                    }
                    else
                    {
                        return new ActionResult<GetCustomerInfoResponse>(false, (int)response.RsCode, new GetCustomerInfoResponse(), response.Message);
                    }
                }
                else
                {
                    return new ActionResult<GetCustomerInfoResponse>(false, (int)proxyResp.StatusCode, new GetCustomerInfoResponse(), proxyResp.ReasonPhrase);
                }
            }
            catch (Exception exp)
            {
                _logCore.StateLog(LogType.Error, "GetCustomerInfoProxyException", exp.Message, exp);
                return new ActionResult<GetCustomerInfoResponse>(false, (int)LogType.Error, new GetCustomerInfoResponse());
            }
        }
        public async Task<ActionResult<GetCustomerAccountsProxyResponse>> GetCustomerAccounts(GetCustomerAccountsProxyRequest request)
        {
            try
            {
                var startTime = DateTime.Now;
                var proxyResp = await Post(request, XApiAddresses.GetCustomerAccounts, "7");
                var responseString = proxyResp.Content.ReadAsStringAsync().Result;

                var responseTime = Convert.ToInt32((DateTime.Now - startTime).TotalMilliseconds);
                _logCore.ActionLog(TransactionType.GetCustomerDepositslist, "GetCustomerAccounts", (int)proxyResp.StatusCode, responseTime, JsonConvert.SerializeObject(request), responseString);
                var response =new GetCustomerDepositsProxyResponse();
                if (proxyResp.StatusCode == HttpStatusCode.OK)
                {
                    response = JsonConvert.DeserializeObject<GetCustomerDepositsProxyResponse>(responseString);
                    if (response.RsCode == (int)BlockDepositProxyStatus.Sucessful)
                    {
                        return GenerateResponse(response);
                    }
                    else
                    {
                        return new ActionResult<GetCustomerAccountsProxyResponse>(false, (int)response.RsCode, new GetCustomerAccountsProxyResponse(), response.Message);
                    }
                }
                else if (proxyResp.StatusCode == HttpStatusCode.BadRequest)
                {
                    response = JsonConvert.DeserializeObject<GetCustomerDepositsProxyResponse>(responseString);
                    return new ActionResult<GetCustomerAccountsProxyResponse>(false, (int)response.RsCode, new GetCustomerAccountsProxyResponse(), response.Message);
                }
                else
                {
                    return new ActionResult<GetCustomerAccountsProxyResponse>(false, (int)proxyResp.StatusCode, new GetCustomerAccountsProxyResponse(), proxyResp.ReasonPhrase);
                }
            }
            catch (Exception exp)
            {
                _logCore.StateLog(LogType.Error, "GetCustomerAccountsProxyException", exp.Message, exp);
                return new ActionResult<GetCustomerAccountsProxyResponse>(false, (int)LogType.Error, new GetCustomerAccountsProxyResponse());
            }
        }
        public async Task<ActionResult<GetAccountInfoProxyResponse>> GetAccountInfo(GetAccountInfoProxyRequest request)
        {
            try
            {
                var startTime = DateTime.Now;
                var proxyResp = await Post(request, XApiAddresses.GetAccountInfo, "");
                var responseString = proxyResp.Content.ReadAsStringAsync().Result;

                var responseTime = Convert.ToInt32((DateTime.Now - startTime).TotalMilliseconds);
                _logCore.ActionLog(TransactionType.GetAccountInfo, "GetAccountInfo", (int)proxyResp.StatusCode, responseTime, JsonConvert.SerializeObject(request), responseString);

                if (proxyResp.StatusCode == HttpStatusCode.OK)
                {
                    var response = JsonConvert.DeserializeObject<GetDepositInfoProxyResponse>(responseString);
                    if (response.RsCode == (int)BlockDepositProxyStatus.Sucessful)
                    {
                        return GenerateResponse(response);
                    }
                    else
                    {
                        return new ActionResult<GetAccountInfoProxyResponse>(false, (int)response.RsCode, new GetAccountInfoProxyResponse(), response.Message);
                    }
                }
                else
                {
                    return new ActionResult<GetAccountInfoProxyResponse>(false, (int)proxyResp.StatusCode, new GetAccountInfoProxyResponse(), proxyResp.ReasonPhrase);
                }
            }
            catch (Exception exp)
            {
                _logCore.StateLog(LogType.Error, "GetAccountInfoProxyException", exp.Message, exp);
                return new ActionResult<GetAccountInfoProxyResponse>(false, (int)LogType.Error, new GetAccountInfoProxyResponse());
            }
        }
        public async Task<ActionResult<Contract.Dto.BlockDepositProxyResponse>> BlockDeposit(Contract.Dto.BlockDepositProxyRequest request)
        {
            try
            {
                var startTime = DateTime.Now;
                Dto.BlockDepositProxyRequest proxyRequest = GenerateRequest(request);
                var proxyResp = await Post(proxyRequest, XApiAddresses.BlockDeposit, "");
                var responseString = proxyResp.Content.ReadAsStringAsync().Result;

                var responseTime = Convert.ToInt32((DateTime.Now - startTime).TotalMilliseconds);
                _logCore.ActionLog(TransactionType.BlockDeposit, "BlockDeposit", (int)proxyResp.StatusCode, responseTime, JsonConvert.SerializeObject(request), responseString);

                if (proxyResp.StatusCode == HttpStatusCode.OK)
                {
                    var response = JsonConvert.DeserializeObject<Dto.BlockDepositProxyResponse>(responseString);
                    if (response.RsCode == (int)BlockDepositProxyStatus.Sucessful)
                    {
                        return GenerateResponse(response);
                    }
                    else
                    {
                        return new ActionResult<Contract.Dto.BlockDepositProxyResponse>(false, (int)response.RsCode, new Contract.Dto.BlockDepositProxyResponse(), response.Message);
                    }
                }
                else
                {
                    return new ActionResult<Contract.Dto.BlockDepositProxyResponse>(false, (int)proxyResp.StatusCode, new Contract.Dto.BlockDepositProxyResponse(), proxyResp.ReasonPhrase);
                }
            }
            catch (Exception exp)
            {
                _logCore.StateLog(LogType.Error, "BlockDepositProxyException", exp.Message, exp);
                return new ActionResult<Contract.Dto.BlockDepositProxyResponse>(false, (int)LogType.Error, new Contract.Dto.BlockDepositProxyResponse());
            }
        }

        public async Task<ActionResult<Contract.Dto.BlockWithdrawProxyResponse>> BlockWithdraw(Contract.Dto.BlockWithdrawProxyRequest request)
        {
            try
            {
                var startTime = DateTime.Now;
                Dto.BlockWithdrawProxyRequest proxyRequest = GenerateRequest(request);
                var proxyResp = await Post(proxyRequest,XApiAddresses.BlockWithdraw, "");
                var responseString = proxyResp.Content.ReadAsStringAsync().Result;
                var responseTime = Convert.ToInt32((DateTime.Now - startTime).TotalMilliseconds);
                _logCore.ActionLog(TransactionType.BlockWithdraw, "BlockWithdraw", (int)proxyResp.StatusCode, responseTime, JsonConvert.SerializeObject(request), responseString);

                if (proxyResp.StatusCode == HttpStatusCode.OK)
                {
                    var response = JsonConvert.DeserializeObject<Dto.BlockWithdrawProxyResponse>(responseString);
                    if (response.RsCode == (int)BlockDepositProxyStatus.Sucessful)
                    {
                        return GenerateResponse(response);
                    }
                    else
                    {
                        return new ActionResult<Contract.Dto.BlockWithdrawProxyResponse>(false, (int)response.RsCode, new Contract.Dto.BlockWithdrawProxyResponse(), response.Message);
                    }
                }
                else
                {
                    return new ActionResult<Contract.Dto.BlockWithdrawProxyResponse>(false, (int)proxyResp.StatusCode, new Contract.Dto.BlockWithdrawProxyResponse(), proxyResp.ReasonPhrase);
                }
            }
            catch (Exception exp)
            {
                _logCore.StateLog(LogType.Error, "BlockWithdrawProxyException", exp.Message,exp);
                return new ActionResult<Contract.Dto.BlockWithdrawProxyResponse>(false, (int)LogType.Error, new Contract.Dto.BlockWithdrawProxyResponse());
            }
        }

        public async Task<ActionResult> MatchNationalCodeWithDepositNumber(MatchNationalCodeWithDepositNumberProxyRequest request)
        {
            var wsResponse = await Post(request, XApiAddresses.MatchNationalCodeWithDepositNumber, "");            
            ActionResult response = null;
            if (wsResponse.StatusCode == HttpStatusCode.OK)
            {
                var responseString = wsResponse.Content.ReadAsStringAsync().Result;
                var proxyResponse = JsonConvert.DeserializeObject<WSResponseMatchNationalCodeWithDepositNumber>(responseString);
                if (proxyResponse != null)
                {
                    response = new ActionResult(proxyResponse.IsSuccess, (int)proxyResponse.RsCode, proxyResponse.Message);
                }
                else
                {
                    response = new ActionResult(false, 0);
                }
            }
            else
            {
                response = new ActionResult(false, (int)wsResponse.StatusCode, wsResponse.ReasonPhrase);
            }
            return response;
        }

        public async Task<ActionResult<GetAccountBalanceProxyResponse>> GetAccountBalance(GetAccountBalanceProxyRequest request)
        {
            var wsResponse = await Post(request, XApiAddresses.AccountBalance, "");            
            ActionResult<GetAccountBalanceProxyResponse> response = null;
            if (wsResponse.StatusCode == HttpStatusCode.OK)
            {
                var responseString = wsResponse.Content.ReadAsStringAsync().Result;
                var proxyResponse = JsonConvert.DeserializeObject<WSResponseGetAccountBalance>(responseString);
                response = GenerateResponse(proxyResponse);
            }
            else
            {
                response = new ActionResult<GetAccountBalanceProxyResponse>(false, (int)wsResponse.StatusCode, new GetAccountBalanceProxyResponse(), wsResponse.ReasonPhrase);
            }
            return response;
        }

        public async Task<ActionResult<InquiryBrokerDepositNumberProxyResponse>> InquiryDelegationStatus(InquiryBrokerDepositNumberProxyRequest request)
        {
            var wsResponse = await Post(request, XApiAddresses.InquiryDelegationStatus, "");
            ActionResult<InquiryBrokerDepositNumberProxyResponse> response = null;
            if (wsResponse.StatusCode == HttpStatusCode.OK)
            {
                var responseString = wsResponse.Content.ReadAsStringAsync().Result;
                var proxyResponse = JsonConvert.DeserializeObject<WSResponseInquiryBrokerDepositNumber>(responseString);
                response = GenerateResponse(proxyResponse);
            }
            else
            {
                response = new ActionResult<InquiryBrokerDepositNumberProxyResponse>(false, (int)wsResponse.StatusCode, new InquiryBrokerDepositNumberProxyResponse(), wsResponse.ReasonPhrase);
            }
            return response;

        }
        #region Private Methods
        private async Task<HttpResponseMessage> Post(object content, string apiAddress, string version)
        {
            var client = _clientFactory.CreateClient("ebank");
            var proxyRequest = new HttpRequestMessage(HttpMethod.Post, apiAddress)
            {
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
            };

            proxyRequest.Headers.Add("ApiKey", _siteSetting.DotinConfig.ApiKey);
            proxyRequest.Headers.Add("Signature", GenerateSignature(content, apiAddress));
            proxyRequest.Headers.Add("Accept-Version", version);


            var proxyResp = await client.SendAsync(proxyRequest, HttpCompletionOption.ResponseHeadersRead);
            //ToDo: Save in ActionLogDB with name of action 
            //var responseString = proxyResp.Content.ReadAsStringAsync().Result;
            return proxyResp;
        }
        private string GenerateSignature(object body, string apiAddress)
        {
            var request = JsonConvert.SerializeObject(body);
            var signature = _signature.Create(request, apiAddress);
            return signature;
        }
        private CustomerType MapCustomerType(string customerType)
        {
            switch (customerType)
            {
                case "RealCustomer":
                    return CustomerType.RealCustomer;
                case "LegalCustomer":
                    return CustomerType.LegalCustomer;
                case "ForeignRealCustomer":
                    return CustomerType.ForeignRealCustomer;
                case "ForeignLegalCustomer":
                    return CustomerType.ForeignLegalCustomer;

                default:
                    return CustomerType.RealCustomer;
            }
        }
        private BankAccountStatus MapAccountStatus(int statusCode)
        {
            //ToDo: Ask from Dotin about other status
            switch (statusCode)
            {
                case 0:
                    return BankAccountStatus.Active;
                case 1:
                    return BankAccountStatus.Inactive;

                default:
                    return BankAccountStatus.Active;
            }
        }
        private ActionResult<GetCustomerInfoResponse> GenerateResponse(GetCustomerInfoProxyResponse request)
        {
            var customerInfo = new GetCustomerInfoResponse
            {
                FirstName = request.ResultData.FirstName,
                CustomerType = MapCustomerType(request.ResultData.CustomerType),
                LastName = request.ResultData.LastName,
                NationalCode = request.ResultData.NationalCode
            };

            return customerInfo;
        }
        private ActionResult<GetCustomerAccountsProxyResponse> GenerateResponse(GetCustomerDepositsProxyResponse request)
        {
            var customerAccounts = new List<CustomerAccountProxy>();
            foreach (var customerAccount in request.ResultData)
            {
                if (customerAccount.DepositNumber != null)
                {
                    customerAccounts.Add(new CustomerAccountProxy
                    {
                        AccountNumber = customerAccount.DepositNumber,
                        AccountIban = customerAccount.DepositIban,
                        AccountState = customerAccount.DepositState,
                        AccountTypeTitle = customerAccount.DepositTypeTitle,
                        CreatorBranchCode = customerAccount.BranchCode,
                        OpeningDate = customerAccount.OpeningDate,
                        WithdrawRight = customerAccount.WithdrawRight,
                        SubscriptionType = MapSubscriptionType(customerAccount.IndividualOrSharedDeposit)
                    });
                }

            }
            return new GetCustomerAccountsProxyResponse()
            {
                Accounts = customerAccounts
            };
        }

        private SubscriptionTypeEnum MapSubscriptionType(string individualOrSharedDeposit)
        {
            switch (individualOrSharedDeposit)
            {
                case "انفرادی":
                    return SubscriptionTypeEnum.Single;
                case "اشتراکی":
                    return SubscriptionTypeEnum.Copartnership;
                default:
                    return SubscriptionTypeEnum.Single;
            }
        }

        private ActionResult<GetAccountInfoProxyResponse> GenerateResponse(GetDepositInfoProxyResponse request)
        {
            var customers = new List<CustomerInfoProxy>();
            var accountInfo = new GetAccountInfoProxyResponse
            {
                Number = request.ResultData.AccountNum,
                CreatorBranchCode = request.ResultData.CreateBranchCode,
                CreateDate = request.ResultData.CreateDate,
                Iban = request.ResultData.Iban,
                Status = MapAccountStatus(request.ResultData.AccountStatus.Code),
                TypeDescription = request.ResultData.AccountType.Description,
                OwnershipTypeId = MapAccountOwnershipType(request.ResultData.AccountOwnershipType.Code),
                AvailableAmount = request.ResultData.AvailableAmount,
                ActualAmount = request.ResultData.ActualAmount,
            };
            foreach (var customer in request.ResultData.CustomerList)
            {
                var customerInfo = new CustomerInfoProxy
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    NationalCode = customer.NationalCode,
                    CustomerType = MapCustomerType(customer.CustomerType)
                };
                customers.Add(customerInfo);
            }
            accountInfo.CustomerInfoProxys = customers;
            return accountInfo;
        }
        private CustomerType MapAccountOwnershipType(int code)
        {
            switch (code)
            {
                case 1:
                    return CustomerType.RealCustomer;
                case 2:
                    return CustomerType.LegalCustomer;
                case 3:
                    return CustomerType.ForeignRealCustomer;
                case 4:
                    return CustomerType.ForeignLegalCustomer;
                default:
                    return CustomerType.RealCustomer;
            }
        }
        private Dto.BlockDepositProxyRequest GenerateRequest(Contract.Dto.BlockDepositProxyRequest request)
        {
            return new Dto.BlockDepositProxyRequest
            {
                DepositNumber = request.AccountNumber,
                ReceiptDate = request.ReceiptLetterDate,
                Title = request.LetterTitle,
                Context = request.LetterContext,
                Number = request.LetterNumber,
                Date = request.LetterDate,
                Deadline = request.LetterDeadline,
                CauseCode = request.BlockReasonTitle,
                BlockOrUnblockPasword = request.BlockOrUnblockPasword ?? "",
                ContextImage = request.LetterContextImage ?? "",
                SwiftCode = request.SwiftCode,
            };
        }
        private Contract.Dto.BlockDepositProxyResponse GenerateResponse(Dto.BlockDepositProxyResponse request)
        {
            var blockDepositResult = new Contract.Dto.BlockDepositProxyResponse
            {
                TraceNumber = request.BlockOrUnBlockLetterId,
                TransactionDate = request.TransactionDate,
            };

            return blockDepositResult;
        }
        private Dto.BlockWithdrawProxyRequest GenerateRequest(Contract.Dto.BlockWithdrawProxyRequest request)
        {
            return new Dto.BlockWithdrawProxyRequest
            {
                DepositNumber = request.AccountNumber,
                ReceiptDate = request.ReceiptLetterDate,
                Title = request.LetterTitle,
                Context = request.LetterContext,
                Number = request.LetterNumber,
                Date = request.LetterDate,
                Deadline = request.LetterDeadline,
                CauseCode = request.BlockReasonTitle,
                BlockOrUnblockPasword = request.BlockOrUnblockPasword ?? "",
                ContextImage = request.LetterContextImage ?? "",
                SwiftCode = request.SwiftCode,
            };
        }

        private ActionResult<Contract.Dto.BlockWithdrawProxyResponse> GenerateResponse(Dto.BlockWithdrawProxyResponse request)
        {
            var blockWithdrawResult = new Contract.Dto.BlockWithdrawProxyResponse
            {
                TraceNumber = request.BlockOrUnBlockLetterId,
                TransactionDate = request.TransactionDate,
            };

            return blockWithdrawResult;
        }

        private ActionResult<GetAccountBalanceProxyResponse> GenerateResponse(WSResponseGetAccountBalance request)
        {
            ActionResult<GetAccountBalanceProxyResponse> result = null;
            if (request != null)
            {
                if (request.IsSuccess)
                {
                    GetAccountBalanceProxyResponse balanceData = new GetAccountBalanceProxyResponse();
                    balanceData.CurrentWithdrawableAmount = request.ResultData.CurrentWithdrawableAmount;
                    balanceData.CurrentAmount = request.ResultData.CurrentAmount;
                    result = new ActionResult<GetAccountBalanceProxyResponse>(request.IsSuccess, request.RsCode, balanceData, request.Message);
                }
                else
                {
                    result = new ActionResult<GetAccountBalanceProxyResponse>(request.IsSuccess, request.RsCode, new GetAccountBalanceProxyResponse(), request.Message);
                }
            }
            else
            {
                result = new ActionResult<GetAccountBalanceProxyResponse>(false, 0, new GetAccountBalanceProxyResponse());
            }
            return result;
        }


        private ActionResult<InquiryBrokerDepositNumberProxyResponse> GenerateResponse(WSResponseInquiryBrokerDepositNumber request)
        {
            ActionResult<InquiryBrokerDepositNumberProxyResponse> result = null;
            if (request != null)
            {
                if (request.IsSuccess)
                {
                    InquiryBrokerDepositNumberProxyResponse proxyresult = new InquiryBrokerDepositNumberProxyResponse();
                    proxyresult.DelegationStatus = request.ResultData.State;

                    result = new ActionResult<InquiryBrokerDepositNumberProxyResponse>(request.IsSuccess, request.RsCode, proxyresult, request.Message);
                }
                else
                {
                    result = new ActionResult<InquiryBrokerDepositNumberProxyResponse>(request.IsSuccess, request.RsCode, new InquiryBrokerDepositNumberProxyResponse(), request.Message);
                }
            }
            else
            {
                result = new ActionResult<InquiryBrokerDepositNumberProxyResponse>(false, 0, new InquiryBrokerDepositNumberProxyResponse());
            }
            return result;
        }

        #endregion
    }
}
