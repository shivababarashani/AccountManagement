using AccountManagement.Framework.Common;
using Microsoft.Extensions.Options;
using Serilog;
using System.Xml.Linq;

namespace QActionLogConsumer.LogManagement
{
    public partial class LogManager : ILogManager
    {
        private readonly SiteSettingBaseViewModel _appSetting;
        private Serilog.ILogger fileLogger { get; set; }
        private DateTime loggerDateTime;
        public LogManager(IOptions<SiteSettingBaseViewModel> options)
        {
            _appSetting = options.Value;
            loggerDateTime = DateTime.Now;
            fileLogger = new LoggerConfiguration()

                .WriteTo.Map(le => loggerDateTime.ToString(_appSetting.LogFileConfig.LogFolderFormat
                ), (folderName, wt) => wt.File(AppDomain.CurrentDomain.BaseDirectory+ _appSetting.LogFileConfig.RootFolder + $"{folderName}" + _appSetting.LogFileConfig.LogFileName, rollingInterval: _appSetting.LogFileConfig.RollingType, fileSizeLimitBytes: _appSetting.LogFileConfig.FileSizeLimitBytes, rollOnFileSizeLimit: _appSetting.LogFileConfig.RollOnFileSizeLimit, retainedFileCountLimit: null))
                .CreateLogger();
        }
        public void Log(LoggType loggType,Exception exception,string message)
        {
            switch (loggType)
            {
                
                case LoggType.Error:
                    fileLogger.Error(exception,message);
                    break;
                case LoggType.Information:
                    fileLogger.Information(exception,message);
                    break;
                case LoggType.Debug:
                    fileLogger.Debug(exception, message);
                    break;
                default:
                    fileLogger.Error(exception, message);
                    break;
            }
            
        }
    }
}
