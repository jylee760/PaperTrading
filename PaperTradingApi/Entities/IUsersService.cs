using PaperTradingApi.Models.DTO;

namespace PaperTradingApi.Entities
{
    public interface IUsersService
    {
        Task<UserDetailsDTO?> GetUser(String Name);
        Task<StockDetailsDTO?> GetUserStock(String Name, String Stock);
        Task<UserOrderDTO?> GetUserOrder(String Name, DateTime timestamp);
        Task<List<UserOrderDTO>> GetUserHistory(String Name);
        Task<List<StockDetailsDTO>> GetAllStock(String Name);
        Task<UserDetailsDTO> AddUser(UserDetailsDTO user);
        Task<UserOrderDTO> AddUserOrder(String Name, UserOrderDTO orders);
        Task<UserDetailsDTO?> AddFunds(String Name, decimal Amount);
    }
}
