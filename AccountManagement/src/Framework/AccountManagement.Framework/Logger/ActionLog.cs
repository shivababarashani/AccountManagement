using AccountManagement.Framework.Common;
using AccountManagement.Framework.QueueProducer;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Framework.Logger
{
    public class ActionLogModel
    {
        public string TraceId { get; set; }
        public TransactionType TransactionTypeId { get; set; }
        public string ActionName { get; set; }
        public int ResponseStatus { get; set; }
        public int ResponseTime { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
    }
}
