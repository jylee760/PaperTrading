using PaperTrading.Models;
using PaperTradingApi.Models;

namespace PaperTrading.Entities.ApiRepositories
{
    public interface IUsersRepository
    {
        Task<UserDetails?> GetUser(string Name);
        Task<StockDetails?> GetUserStock(string Name, string Stock);
        Task<UserOrders?> GetUserOrder(string Name, DateTime timestamp);
        Task<List<UserAllOrders>> GetUserHistory(string Name);
        Task<List<StockDetails>> GetAllStocks(string Name);
        Task<UserDetails> AddUser(UserDetails user);
        Task<UserOrders> CreateNewOrder(UserOrders order);
        Task<UserDetails?> AlterUserMoney(string Name, decimal Price, int Amount, string StockTicker);
        Task<UserDetails?> AddFunds(string Name, decimal Amount);
    }
}
