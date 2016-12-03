using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileUpload.BLL.Models;
using FileUpload.BLL.Services.TransactionService;
using FileUpload.BLL.Services.ValidationService;
using FileUpload.Common.Logging;

namespace FileUpload.BLL
{
    public class FileManager : IFileManager
    {
        private readonly ILogger _logger;
        private readonly ITransactionService _transactionService;
        private readonly IValidationService _validationService;

        public FileManager(ITransactionService transactionService, IValidationService validationService,
            ILogger logger)
        {
            _transactionService = transactionService;
            _validationService = validationService;
            _logger = logger;
        }


        public async Task<ProcessedFile> ProcessCsvFilesAsync(Stream fileContents, string fileName)
        {
            var processedFile = new ProcessedFile
            {
                TotalTransactionsProcessed = 0,
                ErrorsList = new ConcurrentBag<string>()
            };

            try
            {
                if (fileContents != null)
                {
                    string content;
                    var index = 0;

                    using (var reader = new StreamReader(fileContents))
                    {
                        content = await reader.ReadToEndAsync();
                    }

                    var lines =
                        content.Split(new[] {"\n", "\r\n", "Account,", "Description,", "CurrencyCode,", "Amount"},
                            StringSplitOptions.RemoveEmptyEntries);


                    Parallel.For((long) 0, lines.Length, line =>
                    {
                        var fields = lines[line].Split(',');

                        var account = fields[0];
                        var description = fields[1];
                        var currencyCode = fields[2];
                        var amount = fields[3];


                        processedFile.ErrorsList = _validationService.IsValid(new Transaction
                        {
                            Account = account,
                            Description = description,
                            CurrencyCode = currencyCode,
                            Amount = amount
                        });


                        if (processedFile.ErrorsList.Any())
                        {
                            return;
                        }


                        var transaction = new Transaction
                        {
                            Account = account,
                            Amount = amount,
                            CurrencyCode = currencyCode,
                            Description = description
                        };


                        if (_transactionService.AddTransaction(transaction) == -1)
                        {
                            processedFile.TotalTransactionsProcessed = Interlocked.Increment(ref index);
                        }
                    });
                }
            }
            catch (AggregateException ex)
            {
                _logger.Log(
                    $"[FileManager::ProcessedCsvFiles] A aggregateexception has occurred, the error message is {ex.Message}",
                    LogLevel.Error, ex);
                ex.Handle(exc => true);
            }
            catch (Exception exception)
            {
                _logger.Log(
                    $"[FileManager::ProcessedCsvFiles] A system error has occurred, the error message is {exception.Message}",
                    LogLevel.Error, exception);
            }

            return processedFile;
        }
    }
}