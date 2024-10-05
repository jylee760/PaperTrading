using PaperTrading.Models;
using System.ComponentModel.DataAnnotations;

namespace PaperTradingApi.Models.DTO
{
    public class UserOrderDTO
    {
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public String OrderType { get; set; }
        [Required]
        public String StockTicker { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public decimal Price { get; set; }
        public UserOrders ToUserOrders(String user)
        {
            return new UserOrders
            {
                UserName = user,
                Timestamp = Timestamp,
                OrderType = OrderType,
                StockTicker = StockTicker,
                Amount = Amount,
                Price = Price
            };
        }
        public UserAllOrders ToUserAllOrders(String user)
        {
            return new UserAllOrders
            {
                UserName = user,
                Timestamp = Timestamp,
                OrderType = OrderType,
                StockTicker = StockTicker,
                Amount = Amount,
                Price = Price
            };
        }
    }
    public static class UserOrdersExtension
    {
        public static UserOrderDTO ToUserOrderDTO(this UserOrders userOrders)
        {
            return new UserOrderDTO
            {
                Timestamp = userOrders.Timestamp,
                OrderType = userOrders.OrderType,
                StockTicker = userOrders.StockTicker,
                Amount = userOrders.Amount,
                Price = userOrders.Price
            };
        }
        public static UserOrderDTO ToUserOrderDTO(this UserAllOrders userOrders)
        {
            return new UserOrderDTO
            {
                Timestamp = userOrders.Timestamp,
                OrderType = userOrders.OrderType,
                StockTicker = userOrders.StockTicker,
                Amount = userOrders.Amount,
                Price = userOrders.Price
            };
        }
    }
}
