using System.Collections.Generic;
using System.Data;
using Dapper;
using FileUpload.Common.Logging;
using FileUpload.Repository.Infrastructure;
using FileUpload.Repository.Models;

namespace FileUpload.Repository.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger _logger;

        public TransactionRepository(IConnectionFactory connectionFactory, ILogger logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            _logger.Log(
                $"[TransactionRepository::GetTransactions] Entering method",
                LogLevel.Debug);

            using (var connection = _connectionFactory.GetConnection)
            {
                return connection.Query<Transaction>("SELECT * FROM dbo.TRANSACTIONS");
            }
        }


        public int AddTransaction(Transaction transactions)
        {
            _logger.Log(
                $"[TransactionRepository::AddTransaction] Calling method with {transactions}",
                LogLevel.Debug);

            var resultsAfected = 0;
            using (var connection = _connectionFactory.GetConnection)
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Account", transactions.Account);
                parameters.Add("@Desc", transactions.Description);
                parameters.Add("@CurrencyCode", transactions.CurrencyCode);
                parameters.Add("@Amount", transactions.Amount);

                resultsAfected = connection.Execute("dbo.INSERT_TRANSACTIONS", parameters, null, null,
                    CommandType.StoredProcedure);
            }


            return resultsAfected;
        }
    }
}