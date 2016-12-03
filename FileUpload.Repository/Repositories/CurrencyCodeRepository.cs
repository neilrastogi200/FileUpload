using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FileUpload.Common.Logging;
using FileUpload.Repository.Infrastructure;
using FileUpload.Repository.Models;

namespace FileUpload.Repository.Repositories
{
    public class CurrencyCodeRepository : ICurrencyCodeRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger _logger;

        public CurrencyCodeRepository(IConnectionFactory connectionFactory, ILogger logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }


        public IEnumerable<string> GetAlphaNumericCode()
        {
            _logger.Log(
                $"[CurrencyCodeRepository::GetAlphaNumericCode] Entering method",
                LogLevel.Debug);

            using (var connection = _connectionFactory.GetConnection)
            {
                return
                    connection.Query<CurrencyCode>("dbo.GETCURRENCYCODES", null, null, true,
                        commandType: CommandType.StoredProcedure).Select(x => x.AlphaNumericCode);
            }
        }
    }
}