namespace PaperTradingApi.Models
{
    public class StockDetails
    {
        public String UserName { get; set; }
        public String StockTicker { get; set; }
        public int Amount { get; set; }
        public decimal Cost {get; set; }
    }
}
