using AccountManagement.Framework.Common;
using AccountManagement.Framework.QueueProducer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Framework.Logger
{
    public interface ILogCoreFactory 
    {
        void ActionLog(TransactionType transactionType, string actionName, int responseStatus, int responseTime, string requestBody, string responseBody);
        void StateLog(LogType logType, string actionName, string body,Exception exception =null);
    }
    public class LogCoreFactory: ILogCoreFactory
    {
        private readonly IQueueProducer _queueProducer;
        private readonly IHttpContextAccessor _accessor;
        public LogCoreFactory(IQueueProducer queueProducer, IHttpContextAccessor accessor)
        {
            _queueProducer = queueProducer;
            _accessor = accessor;
        }

        public void ActionLog(TransactionType transactionType, string actionName, int responseStatus, int responseTime, string requestBody, string responseBody)
        {
            var log = new ActionLogModel()
            {
                RequestBody = requestBody,
                ResponseBody = responseBody,
                ResponseTime = responseTime,
                ResponseStatus = responseStatus,
                TransactionTypeId = transactionType,
                ActionName = actionName,
                TraceId = _accessor.HttpContext.TraceIdentifier
            };

            _queueProducer.SendMessage("ActionLog", log);
        }
        public void StateLog(LogType logType, string actionName, string body, Exception exception = null)
        {
            var log = new StateLogModel()
            {
                LogType = logType,
                TraceId = _accessor.HttpContext.TraceIdentifier,
                ActionName = actionName,
                Body = body,
                Exception=exception.ToString()
            };

            _queueProducer.SendMessage("StateLog", log);
        }
        
    }
   
}
