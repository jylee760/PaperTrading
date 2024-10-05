using System.ComponentModel.DataAnnotations;

namespace PaperTrading.Models.ui
{
    public class OrderDTO
    {
        public String StockTicker { get; set; }
        [Range(0,int.MaxValue,ErrorMessage ="Amount cannot be negative")]
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
