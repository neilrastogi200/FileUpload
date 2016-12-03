using System.Collections.Generic;
using FileUpload.Repository.Models;

namespace FileUpload.BLL.Services.TransactionService
{
    public interface ITransactionService
    {
        List<Transaction> GetTransactions();

        int AddTransaction(Models.Transaction transactions);
    }
}