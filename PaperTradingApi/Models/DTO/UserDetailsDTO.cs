using System.ComponentModel.DataAnnotations;

namespace PaperTradingApi.Models.DTO
{
    public class UserDetailsDTO
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        public decimal AllTimeMoney { get; set; }
        [Required]
        public decimal CurrentMoney { get; set; }
        public UserDetails ToUserDetails()
        {
            return new UserDetails
            {
                UserName = UserName,
                Password = Password,
                AllTimeMoney = AllTimeMoney,
                CurrentMoney = CurrentMoney
            };
        }
    }
    public static class UserDetailsExtension
    {
        public static UserDetailsDTO ToUserDetailDTO(this UserDetails userDetails)
        {
            return new UserDetailsDTO
            {
                UserName = userDetails.UserName,
                Password = userDetails.Password,
                AllTimeMoney = userDetails.AllTimeMoney,
                CurrentMoney = userDetails.CurrentMoney
            };
        }
    }
}
