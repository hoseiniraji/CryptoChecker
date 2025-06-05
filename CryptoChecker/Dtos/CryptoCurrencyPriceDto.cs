namespace CryptoChecker.Dtos
{
    public class CryptoCurrencyPriceDto
    {
        public string Symbol { get; set; }
        public string BaseCurrency { get; set; }
        public double Price { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
