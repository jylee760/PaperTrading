using PaperTradingApi.Models;

namespace PaperTradingApi.Entities
{
    public interface IUsersRepository
    {
        Task<UserDetails?> GetUser(String Name);
        Task<StockDetails?> GetUserStock(String Name, String Stock);
        Task<UserOrders?> GetUserOrder(String Name, DateTime timestamp);
        Task<List<UserOrders>> GetUserHistory(String Name);
        Task<List<StockDetails>> GetAllStocks(String Name);
        Task<UserDetails> AddUser(UserDetails user);
        Task<UserOrders> CreateNewOrder(UserOrders order);
        Task<UserDetails?> AlterUserMoney(String Name, decimal Amount);
        Task<UserDetails?> AddFunds(String Name, decimal Amount);
    }
}
