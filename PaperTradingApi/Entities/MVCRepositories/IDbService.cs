using PaperTrading.Models.DTO;
using PaperTrading.Models.ui;
using PaperTradingApi.Models.DTO;

namespace PaperTrading.Entities.MVCRepositories
{
    public interface IDbService
    {
        Task<string?> Login(LoginDTO login);
        Task<string?> ValidateUser(UserLoginDTO login, String jwt);
        Task<List<StocksHeld>?> retrieveStocks(String user, String jwt);
        Task<UserDetailsDTO> getUserDetails(String user, String jwt);
        Task<List<UserOrderDTO>> getOrders(String user, String jwt);
        Task<UserOrderDTO> placeOrder(String user, String jwt, UserOrderDTO order);
        Task<UserDetailsDTO> addFunds(String user, String jwt, UserDetailsAddFundsDTO funds);
        Task<UserDetailsDTO> register(UserDetailsDTO details, String jwt);
    }
}
