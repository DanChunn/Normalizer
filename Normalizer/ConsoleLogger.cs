using System;

namespace Normalizer
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception exception);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}