using CryptoChecker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest
{
    public class TestExchangeService : IClassFixture<DependencyFixture>
    {
        private readonly IExchangeService _exchangeService;
        private readonly IConfiguration _configuration;
        public TestExchangeService(DependencyFixture dependencyFixture)
        {
            _exchangeService = dependencyFixture.ServiceProvider.GetRequiredService<IExchangeService>();
            _configuration = dependencyFixture.ServiceProvider.GetRequiredService<IConfiguration>();
        }

        [Fact]
        public async Task TestFetchList()
        {
            IEnumerable<string> list = await _exchangeService.GetCryptoCurrenciesListAsync();
            Assert.NotEmpty(list);
        }

        [Theory]
        [InlineData("BTC", true)]
        [InlineData("UNKNOWN", false)]
        public async Task TestGetPrices(string symbol, bool shouldHasValue)
        {
            var result = await _exchangeService.GetPricesAsync(symbol);
            Assert.Equal(result.Count() > 0, shouldHasValue);
        }

        /// <summary>
        /// Make sure all required configs has valid value
        /// </summary>
        [Fact]
        public void ValidateConfiguration()
        {
            Assert.NotNull(_configuration);
            var keys = new[] {
                   "ExchangeRates_BaseUrl",
                   "ExchangeRates_ApiKey",
                   "CoinMarketCap_BaseUrl",
                   "CoinMarketCap_ApiKey"
            };
            foreach (var key in keys)
            {
                var config = _configuration.GetValue<string>(key);
                Assert.False(string.IsNullOrEmpty(config));
            }
        }
    }
}