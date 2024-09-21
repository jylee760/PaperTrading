using System.ComponentModel.DataAnnotations;

namespace PaperTradingApi.Models.DTO
{
    public class UserDetailsAddFundsDTO
    {
        [Required]
        public decimal Amount { get; set; }
    }
}
