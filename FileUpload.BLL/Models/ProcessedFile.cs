using System.Collections.Concurrent;

namespace FileUpload.BLL.Models
{
    public class ProcessedFile
    {
        public int TotalTransactionsProcessed { get; set; }

        public ConcurrentBag<string> ErrorsList { get; set; }
    }
}