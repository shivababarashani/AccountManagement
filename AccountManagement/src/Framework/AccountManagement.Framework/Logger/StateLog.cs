using AccountManagement.Framework.Common;
using AccountManagement.Framework.QueueProducer;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Framework.Logger
{
    public class StateLogModel
    {
        public string TraceId { get; set; }
        public LogType LogType { get; set; }
        public string ActionName { get; set; }
        public string Body { get; set; }
        public string Exception { get; set; }
    }
   
}

