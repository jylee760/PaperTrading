using System.ComponentModel.DataAnnotations;

namespace PaperTradingApi.Models.DTO
{
    public class StockDetailsDTO
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public String StockTicker { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public StockDetails ToStockDetails()
        {
            return new StockDetails
            {
                UserName = UserName,
                StockTicker = StockTicker,
                Amount = Amount,
                Cost = Cost
            };
        }
    }
    public static class StockDetailsExtension
    {
        public static StockDetailsDTO ToStockDetailsDTO(this StockDetails stockDetails)
        {
            return new StockDetailsDTO
            {
                UserName = stockDetails.UserName,
                StockTicker = stockDetails.StockTicker,
                Amount = stockDetails.Amount,
                Cost = stockDetails.Cost
            };
        }
    }
}
