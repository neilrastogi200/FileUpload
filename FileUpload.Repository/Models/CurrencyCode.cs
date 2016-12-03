namespace FileUpload.Repository.Models
{
    public class CurrencyCode
    {
        public int Id { get; set; }

        public string Entity { get; set; }

        public string Currency { get; set; }

        public string AlphaNumericCode { get; set; }

        public string NumericCode { get; set; }
    }
}