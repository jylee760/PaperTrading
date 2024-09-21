namespace PaperTradingApi.Models
{
    public class UserDetails
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public decimal AllTimeMoney { get; set; }
        public decimal CurrentMoney { get; set; }
    }
}
