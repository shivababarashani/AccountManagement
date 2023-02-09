using AccountManagement.Contract.Dto;
using AccountManagement.Framework.ApiResult;

namespace AccountManagement.Contract.Interfaces.Services
{
    public interface IAccountService
    {
        Task<Guid> Add(AddAccountRequest request);
        Task<GetAccountOwnershipTypeResponse> GetAccountOwnershipTypes();
        Task<GetSubscriptionTypesResponse> GetSubscriptionTypes();
        Task<ActionResult<GetCustomerAccountsResponse>> GetCustomerAccounts(GetCustomerAccountsRequest request);        
        Task<ActionResult<GetAccountInfoResponse>> GetAccountInfo(GetAccountInfoRequest request);
        Task<GetAccountByDemandPacketIdResponse> GetAccountsByDemandPacketId(GetAccountByDemandPacketIdRequest request);
        Task<GetSwiftTypesResponse> GetSwiftTypes();
    }
}
