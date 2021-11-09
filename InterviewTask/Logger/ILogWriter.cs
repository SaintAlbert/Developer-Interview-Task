using System.IO;

namespace InterviewTask.Logger
{
    public interface ILogWriter
    {
        void LogWrite(string logMessage);
        void Log(string logMessage, TextWriter txtWriter);
    }
}
