using AccountManagement.Contract.Dto;
using AccountManagement.Contract.Enums;
using AccountManagement.Contract.Interfaces.Proxy;
using AccountManagement.Contract.Interfaces.Repositories;
using AccountManagement.Contract.Interfaces.Services;
using AccountManagement.Domain.Enums;
using AccountManagement.Framework.ApiResult;

namespace AccountManagement.Service.Services
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IBlockDepositTransactionService _blockDepositTransactionService;
        private readonly IBlockWithdrawTransactionService _blockWithdrawTransactionService;
        private readonly IDemandPacketService _demandPacketService;
        private readonly IXProxy _dotinProxy;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IBlockUnblockReasonRepository _blockUnblockReasonRepository;



        public AccountManagementService(IBlockDepositTransactionService blockDepositTransactionService,
                                        IBlockWithdrawTransactionService blockWithdrawTransactionService,
                                        IDemandPacketService demandPacketService,
                                        IXProxy dotinProxy,
                                        IUnitOfWork unitOfWork,
                                        IBlockUnblockReasonRepository blockUnblockreasonRepository)
        {
            _blockDepositTransactionService = blockDepositTransactionService;
            _blockWithdrawTransactionService = blockWithdrawTransactionService;
            _demandPacketService = demandPacketService;
            _dotinProxy = dotinProxy;
            _unitOfWork = unitOfWork;
            _blockUnblockReasonRepository = blockUnblockreasonRepository;
        }
        #region Public Methods
        public async Task<GetBlockUnblockReasonResponse> GetBlockUnblockReasonListAsync()
        {
            var blockUnblockReasonList = await _blockUnblockReasonRepository.GetBlockUnblockReasonsAsync();
            List<BlockUnblockReasonItem> BlockUnblockReasonItems = new List<BlockUnblockReasonItem>();
            GetBlockUnblockReasonResponse response = new GetBlockUnblockReasonResponse();

            foreach (var item in blockUnblockReasonList)
            {
                BlockUnblockReasonItems.Add(new BlockUnblockReasonItem { Code = item.Id, Title = item.Title });
            }
            response.BlockUnblockReasonItems = BlockUnblockReasonItems;

            return response;
        }
        public async Task<BlockDepositStatus> BlockDeposit(BlockDepositRequest request)
        {
            var blockDepositProxyResult = await BlockDepositProxy(request);
            await AddBlockDepositTransaction(request.AccountId, blockDepositProxyResult);
            await SetDemandStatusDoneTransaction(request.DemandPacketId);
            await SaveChanges();
            return GenerateResponse(blockDepositProxyResult);
        }
        public async Task<BlockWithdrawStatus> BlockWithdraw(BlockWithdrawRequest request)
        {
            var blockWithdrawProxyResult = await BlockWithdrawProxy(request);
            await AddBlockWithdrawTransaction(request.AccountId, blockWithdrawProxyResult);
            await SetDemandStatusDoneTransaction(request.DemandPacketId);
            await SaveChanges();

            return GenerateResponse(blockWithdrawProxyResult);
        }

        #endregion

        #region Private Methods
        private async Task SetDemandStatusDoneTransaction(Guid demandPacketId)
        {
            var request = new SetDemandPacketStatusRequest
            {
                Id = demandPacketId,
                DemandStatusId = DemandStatusEnum.DoneTransaction
            };
            await _demandPacketService.SetDemandPacketStatus(request);
        }
        private async Task AddBlockWithdrawTransaction(Guid accountId, ActionResult<BlockWithdrawProxyResponse> blockInfo)
        {
            var request = new AddBlockWithdrawTransactionRequest
            {
                //ToDo: زمانی که یک جدول برای پیغام ها تعریف کردم اینها تغییر میکنه و وردی متد هم دیگه از این نوع نیست
                Status = blockInfo.StatusCode,
                Description = blockInfo.Message,
                TraceNumber = blockInfo.Data.TraceNumber,
                Date = blockInfo.Data.TransactionDate,
                AccountId = accountId,
            };
            await _blockWithdrawTransactionService.Add(request);
        }

        private BlockWithdrawStatus GenerateResponse(ActionResult<BlockWithdrawProxyResponse> blockInfo)
        {
            //اینجا باید همه استتوس های مربوط به داتین رو چک کنم که بر اساس اونها بگم کدوما دوباره ریترای بشه
            if (blockInfo.IsSuccess)
            {
                return BlockWithdrawStatus.Successful;
            }
            if (blockInfo.StatusCode == 401 || blockInfo.StatusCode == 8)
            {
                return BlockWithdrawStatus.Retry;
            }
            return BlockWithdrawStatus.Unsuccessful;
        }

        private BlockDepositStatus GenerateResponse(ActionResult<BlockDepositProxyResponse> blockInfo)
        {
            //اینجا باید همه استتوس های مربوط به داتین رو چک کنم که بر اساس اونها بگم کدوما دوباره ریترای بشه
            if (blockInfo.IsSuccess)
            {
                return BlockDepositStatus.Successful;
            }
            if (blockInfo.StatusCode == 401 || blockInfo.StatusCode == 8)
            {
                return BlockDepositStatus.Retry;
            }
            return BlockDepositStatus.Unsuccessful;

        }

        private async Task AddBlockDepositTransaction(Guid accountId, ActionResult<BlockDepositProxyResponse> blockInfo)
        {
            var request = new AddBlockDepositTransactionRequest
            {
                AccountId = accountId,
                //ToDo: زمانی که یک جدول برای پیغام ها تعریف کردم اینها تغییر میکنه و وردی متد هم دیگه از این نوع نیست
                Status = blockInfo.StatusCode,
                Description = blockInfo.Message,
                TraceNumber = blockInfo.Data.TraceNumber,
                Date = blockInfo.Data.TransactionDate
            };
            await _blockDepositTransactionService.Add(request);
        }
        private async Task<ActionResult<BlockDepositProxyResponse>> BlockDepositProxy(BlockDepositRequest request)
        {
            var requset = new BlockDepositProxyRequest
            {
                AccountNumber = request.AccountNumber,
                BlockReasonTitle = request.BlockReason.ToString(),
                LetterContext = request.LetterContext,
                LetterDate = request.LetterDate,
                LetterDeadline = request.LetterDeadline,
                LetterNumber = request.LetterNumber,
                LetterTitle = request.LetterTitle,
                ReceiptLetterDate = request.ReceiptLetterDate,
                BlockOrUnblockPasword = request.BlockUnblockPassword,
                LetterContextImage = request.LetterContextImage,
                SwiftCode = request.SwiftType.ToString()
            };
            var blockResult = await _dotinProxy.BlockDeposit(requset);

            //ToDo: اینجا حتما خروجی چک بشه که اگر خروجی موفق نبود تو یه جدول ذخیره بشه
            MapMessageAndStatusCode(blockResult);
            return blockResult;
        }
        private async Task<ActionResult<BlockWithdrawProxyResponse>> BlockWithdrawProxy(BlockWithdrawRequest request)
        {
            var requset = new BlockWithdrawProxyRequest
            {
                AccountNumber = request.AccountNumber,
                BlockReasonTitle = request.BlockReason.ToString(),
                LetterContext = request.LetterContext,
                LetterDate = request.LetterDate,
                LetterDeadline = request.LetterDeadline,
                LetterNumber = request.LetterNumber,
                LetterTitle = request.LetterTitle,
                ReceiptLetterDate = request.ReceiptLetterDate,
                BlockOrUnblockPasword = request.BlockUnblockPassword,
                LetterContextImage = request.LetterContextImage,
                SwiftCode = request.SwiftType.ToString()

            };
            var blockResult = await _dotinProxy.BlockWithdraw(requset);

            //ToDo: اینجا حتما خروجی چک بشه که اگر خروجی موفق نبود تو یه جدول ذخیره بشه
            MapMessageAndStatusCode(blockResult);
            return blockResult;
        }
        private async Task SaveChanges()
        {
            await _unitOfWork.SaveChangesAsync();
        }
        private void MapMessageAndStatusCode(ActionResult<BlockDepositProxyResponse> blockInfo)
        {
            if (!blockInfo.IsSuccess)
            {
                switch (blockInfo.StatusCode)
                {

                    case 1038:
                        blockInfo.Message = "اطلاعات ورودی اشتباه است.";
                        break;
                    case 1390:
                        blockInfo.Message = "تاریخ دریافت نامه با تاریخ دریافت باید یکی باشد";
                        break;
                    case 1735:
                        blockInfo.Message = "تراکنش قبلا انجام شده و موفقیت آمیز بوده است.";
                        break;
                    case 0:
                        blockInfo.StatusCode = 9;
                        blockInfo.Message = "فرمت تاریخ ورودی نامعتبر است.";
                        break;
                    default:
                        blockInfo.StatusCode = 8;
                        blockInfo.Message = "سرویس در دسترس نمی باشد";
                        break;
                }
            }
        }
        private void MapMessageAndStatusCode(ActionResult<BlockWithdrawProxyResponse> blockInfo)
        {
            if (!blockInfo.IsSuccess)
            {
                switch (blockInfo.StatusCode)
                {
                    case 1038:
                        blockInfo.Message = "اطلاعات ورودی اشتباه است.";
                        break;
                    case 1390:
                        blockInfo.Message = "تاریخ دریافت نامه با تاریخ دریافت باید یکی باشد";
                        break;
                    case 1735:
                        blockInfo.Message = "تراکنش قبلا انجام شده و موفقیت آمیز بوده است.";
                        break;
                    case 0:
                        blockInfo.StatusCode = 9;
                        blockInfo.Message = "فرمت تاریخ ورودی نامعتبر است.";
                        break;
                    default:
                        blockInfo.StatusCode = 8;
                        blockInfo.Message = "سرویس در دسترس نمی باشد";
                        break;
                }
            }
        }

        #endregion
    }
}
