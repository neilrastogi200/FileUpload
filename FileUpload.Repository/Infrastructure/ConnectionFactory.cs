using System.Configuration;
using System.Data;
using System.Data.Common;

namespace FileUpload.Repository.Infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["FileUpload"].ConnectionString;

        public IDbConnection GetConnection
        {
            get
            {
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var conn = factory.CreateConnection();
                conn.ConnectionString = _connectionString;
                conn.Open();
                return conn;
            }
        }
    }
}