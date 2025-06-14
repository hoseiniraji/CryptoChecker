﻿namespace CryptoChecker.Dtos.ExchangeRatesApiDtos
{
    public record ExchangeRatesApiListResultDto
    {
        public bool Success { get; set; }
        public long Timestamp { get; set; }
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }
}
