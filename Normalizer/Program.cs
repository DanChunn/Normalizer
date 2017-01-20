using System;

namespace Normalizer
{
    public static class Program
    {
        public static void Main()
        {
                var collector = new ConsoleDataCollector();
                var valueToNormalize = collector.GetValueToNormalize();

                var percentNormalizer = new PercentNormalizer();
                var logger = new ConsoleLogger();

                try
                {
                    var normalizerResult = percentNormalizer.Normalize(valueToNormalize);
                    logger.Log(normalizerResult);
                }
                catch (ArgumentNullException ex)
                {
                    logger.Log(ex);
                }
                catch (FormatException ex)
                {
                    logger.Log(ex);
                }

                Console.WriteLine("Hit any key to exit.");
                Console.ReadLine();
        }
    }

 
}