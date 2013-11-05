using System.IO;
using NUnit.Framework;
using SPHealthChequeConverter.Helpers;

namespace SPHealthChequeConverter.Tests
{
    [TestFixture]
    public class AmountConverterTests
    {
        private  IChequeValueConverter _chequeValueConverter;
        
        [SetUp]
        public void Init()
        {
            _chequeValueConverter = new ChequeValueConverter();
        }

        [Test]
        public void ThrowErrorWhenLessThanZero()
        {
            Assert.Throws<InvalidDataException>(() => _chequeValueConverter.Convert(-10));
        }

        [Test]
        public void HandleZeroValue()
        {            
            Assert.AreEqual("Zero", _chequeValueConverter.Convert(0));
        }

        [TestCase(0.01, "One Cent")]
        [TestCase(0.02, "Two Cents")]
        [TestCase(0.03, "Three Cents")]
        [TestCase(0.04, "Four Cents")]
        [TestCase(0.05, "Five Cents")]
        [TestCase(0.06, "Six Cents")]
        [TestCase(0.07, "Seven Cents")]
        [TestCase(0.08, "Eight Cents")]
        [TestCase(0.09, "Nine Cents")]
        [TestCase(0.10, "Ten Cents")]
        [TestCase(0.11, "Eleven Cents")]
        [TestCase(0.12, "Twelve Cents")]
        [TestCase(0.13, "Thirteen Cents")]
        [TestCase(0.14, "Fourteen Cents")]
        [TestCase(0.15, "Fifteen Cents")]
        [TestCase(0.16, "Sixteen Cents")]
        [TestCase(0.17, "Seventeen Cents")]
        [TestCase(0.18, "Eighteen Cents")]
        [TestCase(0.19, "Nineteen Cents")]
        [TestCase(0.20, "Twenty Cents")]
        [TestCase(0.29, "Twenty Nine Cents")]
        [TestCase(0.36, "Thirty Six Cents")]
        [TestCase(0.42, "Forty Two Cents")]
        [TestCase(0.55, "Fifty Five Cents")]
        [TestCase(0.61, "Sixty One Cents")]
        [TestCase(0.70, "Seventy Cents")]
        [TestCase(0.87, "Eighty Seven Cents")]
        [TestCase(0.94, "Ninety Four Cents")]
        [TestCase(1.0, "One Dollar")]
        [TestCase(1.01, "One Dollar and One Cent")]
        [TestCase(1.46, "One Dollar and Forty Six Cents")]
        [TestCase(11.46, "Eleven Dollars and Forty Six Cents")]
        [TestCase(24.46, "Twenty Four Dollars and Forty Six Cents")]
        [TestCase(100, "One Hundred Dollars")]
        [TestCase(125.33, "One Hundred and Twenty Five Dollars and Thirty Three Cents")]
        [TestCase(325.33, "Three Hundred and Twenty Five Dollars and Thirty Three Cents")]
        [TestCase(1000, "One Thousand Dollars")]
        [TestCase(1000.01, "One Thousand Dollars and One Cent")]
        [TestCase(1145.22, "One Thousand and One Hundred and Forty Five Dollars and Twenty Two Cents")]
        [TestCase(11145.22, "Eleven Thousand and One Hundred and Forty Five Dollars and Twenty Two Cents")]
        [TestCase(200000, "Two Hundred Thousand Dollars")]
        [TestCase(2000000, "Two Million Dollars")]
        [TestCase(1210452345.85, "One Billion and Two Hundred and Ten Million and Four Hundred and Fifty Two Thousand and Three Hundred and Forty Five Dollars and Eighty Five Cents")]
        public void HandleAmount(double amount, string amountStr)
        {            
            Assert.AreEqual(amountStr,_chequeValueConverter.Convert(amount));
        }
    }
}