namespace CryptoChecker.Models
{
    public class CryptoCurrencyPrice
    {
        public int Id { get; set; }
        public int CryptoId { get; set; }
        public CryptoCurrency Crypto { get; set; }
        public int BaseCurrencyId { get; set; }
        public Currency BaseCurrency { get; set; }
        public double Price { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
