using System.Collections.Generic;
using System.Linq;
using ExpressMapper;
using FileUpload.Common.Logging;
using FileUpload.Repository.Models;
using FileUpload.Repository.Repositories;

namespace FileUpload.BLL.Services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly ILogger _logger;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository, ILogger logger)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
        }


        public List<Transaction> GetTransactions()
        {
            _logger.Log(
                $"[TransactionService::GetTransactions] Entering method successfully",
                LogLevel.Info);

            var displayTransactions = _transactionRepository.GetTransactions().ToList();
            return displayTransactions;
        }

        public int AddTransaction(Models.Transaction transactions)
        {
            _logger.Log(
                $"[TransactionService::AddTransaction] Entering method successfully",
                LogLevel.Info);

            var repositoryTransactions = Mapper.Map(transactions, new Transaction());
            return _transactionRepository.AddTransaction(repositoryTransactions);
        }
    }
}