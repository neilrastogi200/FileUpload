using System.Collections.Generic;

namespace FileUpload.Repository.Repositories
{
    public interface ICurrencyCodeRepository
    {
        IEnumerable<string> GetAlphaNumericCode();
    }
}