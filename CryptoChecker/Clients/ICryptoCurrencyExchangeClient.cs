using CryptoChecker.Dtos;

namespace CryptoChecker.Clients
{
    public interface ICryptoCurrencyExchangeClient
    {
        Task<IEnumerable<CryptoCurrencyPriceDto>> GetListAsync();
    }
}
