using System.ComponentModel.DataAnnotations;

namespace CryptoChecker.Models
{
    public class CryptoCurrency
    {
        public int Id { get; set; }
        [StringLength(8), Required]
        public string Symbol { get; set; }

    }
}
