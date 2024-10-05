using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PaperTrading.Models.ui;

namespace PaperTrading.Entities.MVCRepositories
{
    public interface IFinnhubService
    {
        Task<StockProfile?> getStockProfile(string stockTicker, string Token);
        Task<Quote?> getQuote(string stockTicker, string Token);
    }
}
