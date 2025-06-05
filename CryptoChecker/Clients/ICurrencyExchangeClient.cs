using CryptoChecker.Dtos;

namespace CryptoChecker.Clients
{
    public interface ICurrencyExchangeClient
    {
        Task<IEnumerable<CurrencyExchangeRateDto>> GetListAsync();
    }
}
