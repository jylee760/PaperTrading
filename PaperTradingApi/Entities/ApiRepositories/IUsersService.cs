using PaperTradingApi.Models.DTO;

namespace PaperTrading.Entities.ApiRepositories
{
    public interface IUsersService
    {
        Task<UserDetailsDTO?> GetUser(string Name);
        Task<StockDetailsDTO?> GetUserStock(string Name, string Stock);
        Task<UserOrderDTO?> GetUserOrder(string Name, DateTime timestamp);
        Task<List<UserOrderDTO>> GetUserHistory(string Name);
        Task<List<StockDetailsDTO>> GetAllStock(string Name);
        Task<UserDetailsDTO> AddUser(UserDetailsDTO user);
        Task<UserOrderDTO> AddUserOrder(string Name, UserOrderDTO orders);
        Task<UserDetailsDTO?> AddFunds(string Name, decimal Amount);
    }
}
