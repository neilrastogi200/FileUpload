using System.Collections.Concurrent;
using System.Collections.Generic;
using FileUpload.BLL.Models;

namespace FileUpload.BLL.Services.ValidationService
{
    public interface IValidationService
    {
        ConcurrentBag<string> IsValid(Transaction transaction);
    }
}
