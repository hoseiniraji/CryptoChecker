namespace CryptoChecker.Models
{
    public class CurrencyExchangeRate
    {
        public int BaseCurrencyId { get; set; }
        public Currency? BaseCurrency { get; set; }
        public int QuoteId { get; set; }
        public Currency? Quote { get; set; }
        public DateTime Timestamp { get; set; }
        public double Rate { get; set; }
    }
}
