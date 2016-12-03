using System.Collections.Generic;
using FileUpload.BLL.Models;
using FileUpload.BLL.Services.TransactionService;
using FileUpload.BLL.Services.ValidationService;
using FileUpload.Repository.Repositories;
using Moq;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public class ValidationServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockCurrencyCodeRepository = new Mock<ICurrencyCodeRepository>();
            _validationServiceService = new ValidationService(_mockCurrencyCodeRepository.Object);
        }

        private Mock<ICurrencyCodeRepository> _mockCurrencyCodeRepository;
        private IValidationService _validationServiceService;

        [Test]
        public void IsValid_When_Adding_A_Correct_Transaction()
        {
            //Arrange
            var tran = new Transaction
            {
                Account = "Tesco",
                CurrencyCode = "GBP",
                Description = "supermarket",
                Amount = "34.5"
            };

            IEnumerable<string> curencyCodeList = new List<string> {"GBP", "USD"};

            _mockCurrencyCodeRepository.Setup(x => x.GetAlphaNumericCode()).Returns(curencyCodeList);

            var expectedResult = new List<string>();

            //Act
            var actualResult = _validationServiceService.IsValid(tran);

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void IsValid_When_Adding_Empty_Account_Value()
        {
            //Arrange
            var tran = new Transaction
            {
                Account = "",
                CurrencyCode = "GBP",
                Description = "supermarket",
                Amount = "34.5"
            };

            IEnumerable<string> curencyCodeList = new List<string> {"GBP", "USD"};

            _mockCurrencyCodeRepository.Setup(x => x.GetAlphaNumericCode()).Returns(curencyCodeList);

            var expectedResult = new List<string>
            {
                $"This transaction has failed as there are missing values for the columns"
            };

            //Act
            var actualResult = _validationServiceService.IsValid(tran);

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
            Assert.AreEqual(actualResult.Count, expectedResult.Count);
        }


        [Test]
        public void IsValid_When_Adding_Invalid_Amount()
        {
            //Arrange
            var tran = new Transaction
            {
                Account = "tesco",
                CurrencyCode = "GBP",
                Description = "supermarket",
                Amount = "0"
            };

            IEnumerable<string> curencyCodeList = new List<string> {"GBP", "USD"};

            _mockCurrencyCodeRepository.Setup(x => x.GetAlphaNumericCode()).Returns(curencyCodeList);

            var expectedResult = new List<string>
            {
                $"For {tran.Account} this has failed validation, the amount is not in correct format"
            };

            //Act
            var actualResult = _validationServiceService.IsValid(tran);

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
            Assert.AreEqual(actualResult.Count, expectedResult.Count);
        }
    }
}