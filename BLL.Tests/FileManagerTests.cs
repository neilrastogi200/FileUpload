using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using FileUpload.BLL;
using FileUpload.BLL.Models;
using FileUpload.BLL.Services.TransactionService;
using FileUpload.BLL.Services.ValidationService;
using FileUpload.Common.Logging;
using FileUpload.Repository.Repositories;
using Moq;
using NUnit.Framework;
using Transaction = FileUpload.Repository.Models.Transaction;

namespace BLL.Tests
{
    [TestFixture]
    public class FileManagerTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockTransactionservice = new Mock<ITransactionService>();
            _mockValidationService = new Mock<IValidationService>();
            _mockFileManager = new Mock<IFileManager>();
            _fileManager = new FileManager(_mockTransactionservice.Object, _mockValidationService.Object,
                _mockLogger.Object);
        }

        private Mock<ITransactionRepository> _mockTransactionRepository;
        private Mock<ILogger> _mockLogger;
        private Mock<ITransactionService> _mockTransactionservice;
        private Mock<IValidationService> _mockValidationService;
        private Mock<IFileManager> _mockFileManager;
        private IFileManager _fileManager;


        [Test]
        //Not completed
        public void ProcessCsvFiles_Successfully()
        {
            //Arrange
            var testStream =
                new MemoryStream(
                    Encoding.UTF8.GetBytes("Account,Description,CurrencyCode,Amount" + '\n' +
                                           "Tesco,supermarket,GBP,34.5"));
            _mockTransactionRepository.Setup(
                x =>
                    x.AddTransaction(new Transaction
                    {
                        Account = "Tesco",
                        Description = "supermarket",
                        CurrencyCode = "GBP",
                        Amount = Convert.ToDecimal(34.5)
                    })).Returns(-1);

            _mockValidationService.Setup(x => x.IsValid(new FileUpload.BLL.Models.Transaction
            {
                Account = "Tesco",
                Description = "supermarket",
                CurrencyCode = "GBP",
                Amount = "34.5"
            })).Returns(new ConcurrentBag<string>());

            var expectedResult = new ProcessedFile
            {
                ErrorsList = new ConcurrentBag<string>(),
                TotalTransactionsProcessed = 1
            };

            _mockFileManager.Setup(x => x.ProcessCsvFilesAsync(testStream, It.IsAny<string>()))
                .ReturnsAsync(expectedResult);


            //Act
            var result = _fileManager.ProcessCsvFilesAsync(testStream, "Test.csv");

            //Assert
        }
    }
}