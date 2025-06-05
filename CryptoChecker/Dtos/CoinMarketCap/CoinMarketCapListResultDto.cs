using System.Text.Json.Serialization;

namespace CryptoChecker.Dtos.CoinMarketCap
{
    public record CoinMarketCapListResultDto
    {
        public CoinMarketCapListResultStatusDto Status { get; set; }
        public CoinMarketCapSymbolDto[] Data { get; set; }


    }

    public record CoinMarketCapListResultStatusDto
    {
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }
        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }
    }

    public record CoinMarketCapSymbolDto(int Id, string Name, string Symbol, Dictionary<string, CoinMarketCapQuoteDto> Quote);

    public record CoinMarketCapQuoteDto
    {
        public double Price { get; set; }
        [JsonPropertyName("percent_change_1h")]
        public double PercentChange_1h { get; set; }
    }
}
