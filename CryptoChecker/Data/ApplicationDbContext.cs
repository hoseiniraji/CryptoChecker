using CryptoChecker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CryptoChecker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CurrencyExchangeRate>().HasKey(e => new { e.BaseCurrencyId, e.QuoteId });
        }

        public DbSet<Models.CryptoCurrency> CryptoCurrencies { get; set; }
        public DbSet<Models.Currency> Currencies { get; set; }
        public DbSet<Models.CryptoCurrencyPrice> CryptoCurrencyPrices { get; set; }
        public DbSet<CurrencyExchangeRate> CurrencyExchangeRate { get; set; }
    }
}
