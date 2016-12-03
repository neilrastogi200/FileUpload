namespace FileUpload.Repository.Models
{
    public class Transaction
    {
        public int AccountId { get; set; }

        public string Account { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCode { get; set; }
    }
}