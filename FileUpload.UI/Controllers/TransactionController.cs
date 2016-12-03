using System.Linq;
using System.Web.Mvc;
using FileUpload.BLL.Services.TransactionService;
using FileUpload.UI.ViewModels;

namespace FileUpload.UI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: Transaction
        public ActionResult DisplayTransactions()
        {
            var transactionList = _transactionService.GetTransactions();

            return View(transactionList.Select(x => new TransactionViewNodel
            {
                Account = x.Account,
                Amount = x.Amount,
                CurrencyCode = x.CurrencyCode,
                Description = x.Description
            }));
        }
    }
}