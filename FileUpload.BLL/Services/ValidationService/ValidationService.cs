using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FileUpload.BLL.Models;
using FileUpload.Repository.Repositories;

namespace FileUpload.BLL.Services.ValidationService
{
    public class ValidationService : IValidationService
    {
        private readonly ICurrencyCodeRepository _currencyCodeRepository;
      

        public ValidationService(ICurrencyCodeRepository currencyCodeRepository)
        {
            _currencyCodeRepository = currencyCodeRepository;
        }

        public List<string> IsValid(Transaction transaction)
        { 
            //Change back to List now resolved issue due to scope of the variable.
            List<string> errorList = new List<string>();

            if (string.IsNullOrEmpty(transaction.Account) || string.IsNullOrEmpty(transaction.Amount) ||
                string.IsNullOrEmpty(transaction.Description) || string.IsNullOrEmpty(transaction.CurrencyCode))
            {
                errorList.Add($"This transaction has failed as there are missing values for the columns");
            }

            if (ValidateAmount(transaction.Amount) == 0)
            {
                errorList.Add(
                    $"For {transaction.Account} this has failed validation, the amount is not in correct format");
            }

            var validCurrencyCodesList = GetCurrencyCodes();

            if (!validCurrencyCodesList.Contains(transaction.CurrencyCode))
            {
                errorList.Add($"The{transaction.CurrencyCode} is invalid format");
            }


            return errorList;
        }

        //Can add caching for further subsequent calls improvement for performance. 
        private IList<string> GetCurrencyCodes()
        {
            var validCurencyCodes = _currencyCodeRepository.GetAlphaNumericCode().ToList();
            return validCurencyCodes;
        }

        private decimal ValidateAmount(string amount)
        {
            decimal outAmount;

            return !decimal.TryParse(amount, out outAmount) ? 0 : outAmount;
        }
    }
}