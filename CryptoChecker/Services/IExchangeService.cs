using CryptoChecker.Dtos;

namespace CryptoChecker.Services
{
    public interface IExchangeService
    {
        Task<IEnumerable<CryptoCurrencyPriceDto>> GetPricesAsync(string cryptoCurrencySymbol);
        Task<IEnumerable<string>> GetCryptoCurrenciesListAsync(); 
    }
}
