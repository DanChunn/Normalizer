using System;
using System.Text;

namespace Normalizer
{
    public interface INormalizer
    {
        string Normalize(string value);
    }

    public class PercentNormalizer : INormalizer
    {
        /// <summary>
        ///     Normalizes a string value for a percentage expressed as a number with or without a percent symbol
        ///     to a decimal string representation.
        ///     -Whitespace is ignored.
        ///     -If present, the percent sign must appear after the number.
        ///     -Commas may be used a number separator
        /// </summary>
        /// <param name="value">The value To normlaize</param>
        /// <returns>Normalized percent as a decimal string</returns>
        /// <exception cref="ArgumentNullException">The value is null, empty or whitespace.</exception>
        /// <exception cref="FormatException">The value is not a valid percent representation.</exception>
        public string Normalize(string value)
        {
            //Examples:
            //  given "12%" returns "0.12"
            //  given "1,345,678.001234" returns "1345678.001234"
            //  given "% 12" throws FormatException
            //  given "  " throws ArgumentNullException
            //  given "        0.1  \t        % " returns "0.001"
            //  given "-1" returns "-1"


            string str = Clean(value);
            str = ConvertPercent(str);
            return str;

            //throw new ArgumentNullException();
        }


        /// <summary>
        ///   Cleans the string of everything but numbers, percent signs, negative signs, and point signs.
        ///   Ensures that percent, negative, and point signs are in proper places and are limited to 1 each.
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>Cleaned string of the value with non-number related symbols removed.</returns>
        /// <exception cref="ArgumentNullException">The value is null, empty or whitespace.</exception>
        /// <exception cref="FormatException">The value is not a valid percent representation.</exception>
        public string Clean(string value)
        {
            
            //If trimmed value is empty, then throw a ArgumentNullException
            value = value.Trim();
            if(value.Length < 1 || value == null)
            {
                throw new ArgumentNullException();
            }


            StringBuilder sb = new StringBuilder();

            //Count of negative, point, and percent signs used to keep track of string integrity
            int negCount = 0;
            int dotCount = 0;
            int percentCount = 0;

            //Iterate through the string to add the correct characters to our StringBuilder, while making sure
            //non-numbers are in the correct places and have the correct count
            for(int i = 0; i < value.Length; i++)
            {
                char c = value[i];

                if(c == '-')
                {
                    if(sb.Length == 0)
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        throw new FormatException();
                    }
                    negCount++;
                }

                else if(c == '.')
                {
                    if(dotCount == 0)
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        throw new FormatException();
                    }
                    dotCount++;
                }

                else if (c == '%')
                {
                    if(percentCount == 0)
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        throw new FormatException();
                    }
                    percentCount++;
                }

                else if(c >= '0' && c <= '9')
                {
                    sb.Append(c);
                }
            }


            string str = sb.ToString();

            //Checks if percent sign is in correct location
            if(percentCount > 0)
            {
                if(str.IndexOf('%') != str.Length - 1)
                {
                    throw new FormatException();
                }
            }

            //If total number of signs is >= to str.Length, then there are no numbers in the string
            if ((negCount + dotCount + percentCount) >= str.Length)
            {
                throw new FormatException(); 
            }

            //If string length is less than 1, then it's a null argument
            if (str.Length < 1)
            {
                throw new ArgumentNullException();
            }

            return str;
        }

        /// <summary>
        ///     Checks if a string has a percent sign at the end. 
        ///     If not, return with the number representation with proper zeroes or decimal symbols.
        ///     ie: .4 => 0.4, 1111. => 1111
        ///     If so, convert the string to a decimal representation of the percent.
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>Normalized percent as a decimal string</returns>
        public string ConvertPercent(string value)
        {

            if (value[value.Length - 1] != '%')
            {
                //return value;
                decimal num1 = Decimal.Parse(value);
                return num1.ToString();
            }

            value = value.Substring(0, value.Length - 1);
            decimal num = Decimal.Parse(value);
            num = num / 100;

            return num.ToString();
        }
    }
}