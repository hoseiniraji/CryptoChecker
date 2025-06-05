using Humanizer.Configuration;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Timeout;
using System.Diagnostics;

namespace CryptoChecker.Clients
{
    public static class Extensions
    {
        public static IServiceCollection AddCurrencyExchangeClient(this IServiceCollection services, IConfiguration configuration)
        {
            string baseUrl = configuration.GetValue<string>("ExchangeRates_BaseUrl") ?? string.Empty;
            services.UseCustomHttpClient<ICurrencyExchangeClient, ExchangeRateClient>(baseUrl);
            return services;
        }

        public static IServiceCollection AddCryptoCurrencyExchangeClient(this IServiceCollection services, IConfiguration configuration)
        {
            string baseUrl = configuration.GetValue<string>("CoinMarketCap_BaseUrl") ?? string.Empty;
            services.UseCustomHttpClient<ICryptoCurrencyExchangeClient, CoinMarketCapClient>(baseUrl);
            return services;
        }

        private static IServiceCollection UseCustomHttpClient<TClient, Implementation>(this IServiceCollection services, string baseUrl)
            where TClient : class
            where Implementation : class, TClient
        {
            services.AddHttpClient<TClient, Implementation>((factory, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
                    .AddTransientHttpErrorPolicy(_builder =>
                    _builder.Or<TimeoutRejectedException>()
                    .WaitAndRetryAsync(2,
                    retAttm => TimeSpan.FromSeconds(Math.Pow(2, retAttm)),
                    onRetry: (outcome, timespan, retryAttempt) =>
                    {
                        Debug.WriteLine($"Wait for {timespan.TotalSeconds} seconds...");
                    }
                    ))
                    .AddTransientHttpErrorPolicy(_builder => _builder.Or<TimeoutRejectedException>().CircuitBreakerAsync(
                        3,
                        TimeSpan.FromSeconds(15),
                        onBreak: (outcome, timespan) =>
                        {
                            Debug.WriteLine($"Opening the circuit for {timespan.TotalSeconds} seconds...");
                        },
                        onReset: () =>
                        {
                            Debug.WriteLine("Closing the circuit...");
                        })
                    )
                    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(5))
                    ;

            return services;
        }
    }
}
