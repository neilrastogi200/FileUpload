using System.IO;
using System.Threading.Tasks;
using FileUpload.BLL.Models;

namespace FileUpload.BLL
{
    public interface IFileManager
    {
        Task<ProcessedFile> ProcessCsvFilesAsync(Stream fileContents, string fileName);
    }
}