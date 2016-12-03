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
        private readonly ConcurrentBag<string> _errorList = new ConcurrentBag<string>();

        public ValidationService(ICurrencyCodeRepository currencyCodeRepository)
        {
            _currencyCodeRepository = currencyCodeRepository;
        }

        public ConcurrentBag<string> IsValid(Transaction transaction)
        {
            if (string.IsNullOrEmpty(transaction.Account) || string.IsNullOrEmpty(transaction.Amount) ||
                string.IsNullOrEmpty(transaction.Description) || string.IsNullOrEmpty(transaction.CurrencyCode))
            {
                _errorList.Add($"This transaction has failed as there are missing values for the columns");
            }

            if (ValidateAmount(transaction.Amount) == 0)
            {
                _errorList.Add(
                    $"For {transaction.Account} this has failed validation, the amount is not in correct format");
            }

            var validCurrencyCodesList = GetCurrencyCodes();

            if (!validCurrencyCodesList.Contains(transaction.CurrencyCode))
            {
                _errorList.Add($"The{transaction.CurrencyCode} is invalid format");
            }


            return _errorList;
        }

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