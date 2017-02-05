using System.Collections.Concurrent;
using System.Collections.Generic;
using FileUpload.BLL.Models;

namespace FileUpload.BLL.Services.ValidationService
{
    public interface IValidationService
    {
        List<string> IsValid(Transaction transaction);
    }
}
