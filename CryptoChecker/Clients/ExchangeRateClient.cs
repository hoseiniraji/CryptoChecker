using CryptoChecker.Dtos;
using CryptoChecker.Dtos.ExchangeRatesApiDtos;

namespace CryptoChecker.Clients
{
    public class ExchangeRateClient : ICurrencyExchangeClient
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;
        private readonly string _symbols;
        public ExchangeRateClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration.GetValue<string>("ExchangeRates_ApiKey", string.Empty);
            _symbols = configuration.GetValue<string>("ExchangeRates_Symbols", "USD") ?? "USD";
        }

        public async Task<IEnumerable<CurrencyExchangeRateDto>> GetListAsync()
        {
            string baseCurrency = "USD";
            // base type not supported in free plan and EUR as default currency is available only.
            ExchangeRatesApiListResultDto? response =
                await _httpClient.GetFromJsonAsync<ExchangeRatesApiListResultDto>($"latest?access_key={_apiKey}&symbols={_symbols}")
                ?? throw new NullReferenceException("Empty result from GetListAsync in ExchangeRateClient");

            if (!response.Rates.ContainsKey(baseCurrency) || response.Rates[baseCurrency] == 0)
            {
                throw new Exception("Insufficient resources fetched in ExchangeRateClient");
            }

            var USD_EUR = response.Rates[baseCurrency];

            var result = response.Rates.Select(r => new CurrencyExchangeRateDto()
            {
                BaseSymbol = baseCurrency,
                QuoteSymbol = r.Key,
                ExChangeRate = r.Value / USD_EUR,
                Timestamp = response.Date,
            }).ToList();

            result.Add(new CurrencyExchangeRateDto()
            {
                BaseSymbol = baseCurrency,
                QuoteSymbol = "EUR",
                ExChangeRate = 1 / USD_EUR,
                Timestamp = response.Date,
            });

            return result;
        }
    }
}
