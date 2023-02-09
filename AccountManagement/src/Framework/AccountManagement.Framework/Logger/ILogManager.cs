using static QActionLogConsumer.LogManagement.LogManager;

namespace QActionLogConsumer.LogManagement
{
    public interface ILogManager
    {
        void Log(LoggType loggType,Exception exception, string message);
    }
}
