using System.ComponentModel.DataAnnotations;

namespace PaperTradingApi.Models.DTO
{
    public class UserDetailsAddFundsDTO
    {
        [Required]
        [Range(0,int.MaxValue,ErrorMessage ="Amount cannot be negative")]
        public decimal Amount { get; set; }
    }
}
