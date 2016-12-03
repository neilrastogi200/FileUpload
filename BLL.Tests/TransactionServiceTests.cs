using System;
using System.Collections.Generic;
using FileUpload.BLL.Services.TransactionService;
using FileUpload.Common.Logging;
using FileUpload.Repository.Models;
using FileUpload.Repository.Repositories;
using Moq;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public class TransactionServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _transactionService = new TransactionService(_mockTransactionRepository.Object, _mockLogger.Object);
        }

        private Mock<ILogger> _mockLogger;
        private Mock<ITransactionRepository> _mockTransactionRepository;
        private ITransactionService _transactionService;

        [Test]
        public void Gettransactions_Displayed_Succesfully()
        {
            //Arrange
            var expectedResult = new List<Transaction>
            {
                new Transaction
                {
                    Account = "tesco",
                    Amount = Convert.ToDecimal(45.6),
                    CurrencyCode = "GBP",
                    Description = "saaaa"
                },
                new Transaction
                {
                    Account = "barclays",
                    Amount = Convert.ToDecimal(450.6),
                    CurrencyCode = "USD",
                    Description = "safffaaa"
                },
                new Transaction
                {
                    Account = "halfiax",
                    Amount = Convert.ToDecimal(459.6),
                    CurrencyCode = "EUR",
                    Description = "saatttttaa"
                }
            };

            _mockTransactionRepository.Setup(x => x.GetTransactions()).Returns(expectedResult);

            //Act
            var actualResult = _transactionService.GetTransactions();

            //Verify

            _mockLogger.Verify(x => x.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), null), Times.Once);
            _mockTransactionRepository.Verify(x => x.GetTransactions(), Times.Once);
            Assert.AreEqual(actualResult, expectedResult);
            Assert.AreEqual(actualResult.Count, expectedResult.Count);
        }
    }
}