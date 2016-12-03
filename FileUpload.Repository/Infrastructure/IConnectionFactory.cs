using System.Data;

namespace FileUpload.Repository.Infrastructure
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}