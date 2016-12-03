using System.Threading.Tasks;
using System.Web.Mvc;
using FileUpload.BLL;
using FileUpload.UI.ViewModels;

namespace FileUpload.UI.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IFileManager _fileManager;

        public FileUploadController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        // GET: FileUpload
        public ActionResult UploadFile()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> UploadFile(FileUploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.File?.FileName))
            {
                var processedTransactions =
                    await _fileManager.ProcessCsvFilesAsync(model.File.InputStream, model.File.FileName);

                if (processedTransactions.ErrorsList.Count == 0)
                {
                    ViewBag.SuccessMessage =
                        $"Transactions processed successfully. The total transactions are {processedTransactions.TotalTransactionsProcessed}";
                }
                else
                {
                    ViewBag.ErrorMessage =
                        $"Number of transactions processed successfully are {processedTransactions.TotalTransactionsProcessed} There were some failures, the specific errors are:";

                    foreach (var error in processedTransactions.ErrorsList)
                    {
                        ViewBag.ValidationError = ViewBag.ValidationError + " " + error;
                    }

                    model.ErrorList = processedTransactions.ErrorsList;
                }
            }

            return View(model);
        }
    }
}