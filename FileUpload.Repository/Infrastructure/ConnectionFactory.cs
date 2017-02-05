using System.Configuration;
using System.Data;
using System.Data.Common;

namespace FileUpload.Repository.Infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["FileUpload"].ConnectionString;


        //Notes Advantage of this method is it allows you to get the correct connectionstring, if you had multiple ones using same providerName within 
        //web.Config. Possibly a method would be better than a property debatable?
        //DBProviderFactories returns the abstract class DbFactory, using the factory pattern, e.g. the command CreateCommand would be worked out for which 
        //implementation type it needs using Oracle or SqlServer version.  
        public IDbConnection GetConnection
        {
            get
            {
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var conn = factory.CreateConnection();
                if (conn != null)
                {
                    conn.ConnectionString = _connectionString;
                    conn.Open();
                    return conn;
                }

                return null;
            }
        }
    }
}