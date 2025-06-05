namespace CryptoChecker.Dtos
{
    public record CurrencyExchangeRateDto
    {
        public string BaseSymbol { get; set; }
        public string QuoteSymbol { get; set; }
        public double ExChangeRate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
