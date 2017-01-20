using System;

namespace Normalizer
{
    public interface INormalizerResult
    {
        string NormalizedValue { get; }
        bool Success { get; }
        string OriginalValue { get; }
        Exception Exception { get; }
    }

    public class NormalizerResult : INormalizerResult
    {
        public NormalizerResult(string originalValue, string normalizedValue)
            : this(originalValue)
        {
            NormalizedValue = normalizedValue;
        }

        public NormalizerResult(string originalValue, Exception normalizationException)
            : this(originalValue)
        {
            Exception = normalizationException;
        }

        private NormalizerResult(string originalValue)
        {
            OriginalValue = originalValue;
        }

        public string NormalizedValue { get; private set; }

        public bool Success
        {
            get { return Exception == null; }
        }

        public string OriginalValue { get; private set; }
        public Exception Exception { get; private set; }
    }
}