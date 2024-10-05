using PaperTradingApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaperTrading.Models
{
    public class UserAllOrders
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
