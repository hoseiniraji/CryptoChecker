using System.Diagnostics;
using CryptoChecker.Clients;
using CryptoChecker.Models;
using CryptoChecker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoChecker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExchangeService _exchangeService;

        public HomeController(ILogger<HomeController> logger, IExchangeService exchangeService)
        {
            _logger = logger;
            _exchangeService = exchangeService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _exchangeService.GetCryptoCurrenciesListAsync();
            ViewBag.CryptoCurrenciesList = list;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
