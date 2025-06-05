using CryptoChecker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoChecker.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoPriceController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;
        public CryptoPriceController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpGet("{symbol}")]
        public async Task<IEnumerable<Dtos.CryptoCurrencyPriceDto>> GetPrice(string symbol)
        {
            var result = await _exchangeService.GetPricesAsync(symbol);
            return result;
        }

    }
}
