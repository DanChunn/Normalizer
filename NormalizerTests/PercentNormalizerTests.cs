using Microsoft.VisualStudio.TestTools.UnitTesting;
using Normalizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Unit testing on PercentNormalizer.
/// </summary>

//Examples:
//  given "12%" returns "0.12"
//  given "1,345,678.001234" returns "1345678.001234"
//  given "% 12" throws FormatException
//  given "  " throws ArgumentNullException
//  given "        0.1  \t        % " returns "0.001"
//  given "-1" returns "-1"

namespace Normalizer.Tests
{
    [TestClass()]
    public class PercentNormalizerTests
    {

        [TestMethod()]
        public void NormalizeTest_NormalPercent_Valid()
        {
            //arrange
            string value = "12%";
            string expected = "0.12";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_NormalCommas_Valid()
        {
            //arrange
            string value = "1,345,678,001,234";
            string expected = "1345678001234";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_UnNormalCommas_Valid()
        {
            //arrange
            string value = "1345678,001,234";
            string expected = "1345678001234";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_NormalPointStart_Valid()
        {
            //arrange
            string value = ".1345678001234";
            string expected = "0.1345678001234";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_NormalPointMid_Valid()
        {
            //arrange
            string value = "1345678.001234";
            string expected = "1345678.001234";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_NormalPointEnd_Valid()
        {
            //arrange
            string value = "1345678001234.";
            string expected = "1345678001234";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_NormalNegative_Valid()
        {
            //arrange
            string value = "-1";
            string expected = "-1";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_NormalMixed1_Valid()
        {
            //arrange
            string value = "1345678001234.00%";
            string expected = "13456780012.34";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_NormalMixed2_Valid()
        {
            //arrange
            string value = "-1,345,678,001,234.00%";
            string expected = "-13456780012.34";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_UncleanInput1_Valid()
        {
            //arrange
            string value = "        0.1  \t        % ";
            string expected = "0.001";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_UncleanInput2_Valid()
        {
            //arrange
            string value = "   -     0.1  \t        % ";
            string expected = "-0.001";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_UncleanInput3_Valid()
        {
            //arrange
            string value = "a-bc1345678001234.00%";
            string expected = "-13456780012.34";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_UncleanInput4_Valid()
        {
            //arrange
            string value = "a-bc1345678001234.%";
            string expected = "-13456780012.34";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NormalizeTest_UncleanInput5_Valid()
        {
            //arrange
            string value = "-.abc1345678001234%";
            string expected = "-0.001345678001234";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            //assert
            Assert.AreEqual(expected, actual);
        }



        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_InvalidPercentError1_Throws()
        {   
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = "% 12";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_InvalidPercentError2_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = "1%2%";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_InvalidPointError1_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = "1.1.";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_InvalidPointError2_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = ".1.1";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_InvalidNegError1_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = "-1-1";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_InvalidNegError2_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = "1-";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NormalizeTest_WhiteSpaceError1_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = " ";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NormalizeTest_WhiteSpaceError2_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = "        ";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NormalizeTest_WhiteSpaceError3_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = "";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_NoNumbersJustSymbols1_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = "    -.%    ";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_NoNumbersJustSymbols2_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = "%";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_NoNumbersJustSymbols3_Throws()
        {
            // arrange
            var percentNormalizer = new PercentNormalizer();
            string value = "abdsdad";

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_UncleanInput1_Throws()
        {
            //arrange
            string value = ".a-bc1345678001234%";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NormalizeTest_UncleanInput2_Throws()
        {
            //arrange
            string value = "-.abc134%5678001234";
            var percentNormalizer = new PercentNormalizer();

            //act
            string actual = percentNormalizer.Normalize(value);

            // assert is handled by the ExpectedException
        }

    }
}