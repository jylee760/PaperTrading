using System.ComponentModel.DataAnnotations.Schema;

namespace PaperTradingApi.Models
{
    public class UserOrders
    {
        public String UserName { get; set; }
        [ForeignKey("UserName")]
        public virtual UserDetails? User { get; set; }

        public DateTime Timestamp { get; set; }
        public String OrderType { get; set; }
        public String StockTicker { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
