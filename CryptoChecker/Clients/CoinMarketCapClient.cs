using CryptoChecker.Data;
using CryptoChecker.Dtos;
using CryptoChecker.Dtos.CoinMarketCap;

namespace CryptoChecker.Clients
{
    public class CoinMarketCapClient : ICryptoCurrencyExchangeClient
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;
        private const string GetList_Api = "cryptocurrency/listings/latest?start=1&limit=100&convert=USD";
        public CoinMarketCapClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration.GetValue<string>("CoinMarketCap_ApiKey", string.Empty);
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IEnumerable<CryptoCurrencyPriceDto>> GetListAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<CoinMarketCapListResultDto>(GetList_Api);
                if (response == null) throw new NullReferenceException("Empty result from CoinMarketCap.GetListAsync");

                if (response.Status.ErrorCode > 0) throw new Exception(response.Status.ErrorMessage);

                var result = response.Data.Select(d => d.Quote.Count > 0 ? new CryptoCurrencyPriceDto()
                {
                    BaseCurrency = d.Quote.Keys.First(),
                    Price = d.Quote.First().Value.Price,
                    Symbol = d.Symbol,
                    TimeStamp = response.Status.Timestamp,
                } : null).ToArray();

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
