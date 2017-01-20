using System;

namespace Normalizer
{
    public interface IDataCollector
    {
        string GetValueToNormalize();
    }

    public class ConsoleDataCollector : IDataCollector
    {
        public string GetValueToNormalize()
        {
            Console.WriteLine("Enter Value to normalize:");
            var line = Console.ReadLine();
            return line;
        }
    }
}