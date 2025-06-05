using CryptoChecker.Clients;
using CryptoChecker.Dtos;

namespace CryptoChecker.Services
{
    public class ExchangeService(ICryptoCurrencyExchangeClient cryptoCurrencyExchange, ICurrencyExchangeClient currencyExchange, ILogger<ExchangeService> logger)
        : IExchangeService
    {
        public DateTime? CurrenciesTimestamp { get; set; }
        public DateTime? CryptoCurrenciesTimestamp { get; set; }
        private const int renewDuration = -5;

        // use local collections as cache list
        private IEnumerable<CryptoCurrencyPriceDto> CryptoCurrencyPrices = [];
        private IEnumerable<CurrencyExchangeRateDto> CurrencyExchangeRates = [];
        private IEnumerable<string> CryptoCurrencyList = [];
        public async Task<IEnumerable<CryptoCurrencyPriceDto>> GetPricesAsync(string cryptoCurrencySymbol)
        {
            await RenewCryptoCurrencyPrices();
            await RenewCurrencyExchangeRates();

            var crypto = CryptoCurrencyPrices.FirstOrDefault(c => c.Symbol == cryptoCurrencySymbol);
            if (crypto == null) return [];

            var result = CurrencyExchangeRates.Select(e => new CryptoCurrencyPriceDto()
            {
                Symbol = cryptoCurrencySymbol,
                BaseCurrency = e.QuoteSymbol,
                TimeStamp = e.Timestamp,
                Price = Math.Round(e.ExChangeRate * crypto.Price, 2),
            });

            return result;
        }

        private async Task RenewCryptoCurrencyPrices()
        {
            if (CryptoCurrenciesTimestamp == null || CryptoCurrenciesTimestamp < DateTime.Now.AddMinutes(renewDuration))
            {
                try
                {
                    CryptoCurrencyPrices = await cryptoCurrencyExchange.GetListAsync();
                    var list = CryptoCurrencyPrices.Select(x => x.Symbol).ToList();
                    list.Insert(0, "");

                    CryptoCurrencyList = list;

                    CryptoCurrenciesTimestamp = DateTime.Now;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error on RenewCryptoCurrencyPrices");
                }
            }
        }

        private async Task RenewCurrencyExchangeRates()
        {
            if (CurrenciesTimestamp == null || CurrenciesTimestamp < DateTime.Now.AddMinutes(renewDuration))
            {
                try
                {
                    CurrencyExchangeRates = await currencyExchange.GetListAsync();
                    CurrenciesTimestamp = DateTime.Now;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error on RenewCryptoCurrencyPrices");
                }
            }
        }

        public async Task<IEnumerable<string>> GetCryptoCurrenciesListAsync()
        {
            await RenewCryptoCurrencyPrices();
            return CryptoCurrencyList;
        }
    }
}
