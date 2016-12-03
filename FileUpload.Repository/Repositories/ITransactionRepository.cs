using System.Collections.Generic;
using FileUpload.Repository.Models;

namespace FileUpload.Repository.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetTransactions();

        int AddTransaction(Transaction transactions);
    }
}