using PaperTradingApi.Models.DTO;
using PaperTradingApi.Models;
using PaperTrading.Models;

namespace PaperTrading.Entities.ApiRepositories
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository) { _usersRepository = usersRepository; }
        public async Task<UserDetailsDTO?> GetUser(string Name)
        {
            UserDetails? user = await _usersRepository.GetUser(Name);
            if (user == null)
            {
                return null;
            }
            return user.ToUserDetailDTO();
        }

        public async Task<StockDetailsDTO?> GetUserStock(string Name, string Stock)
        {
            StockDetails? userStock = await _usersRepository.GetUserStock(Name, Stock);
            if (userStock == null)
            {
                return null;
            }
            return userStock.ToStockDetailsDTO();
        }
        public async Task<UserOrderDTO?> GetUserOrder(string Name, DateTime timestamp)
        {
            UserOrders? order = await _usersRepository.GetUserOrder(Name, timestamp);
            if (order == null)
            {
                return null;
            }
            return order.ToUserOrderDTO();
        }

        public async Task<List<UserOrderDTO>> GetUserHistory(string Name)
        {
            List<UserAllOrders> orders = await _usersRepository.GetUserHistory(Name);
            List<UserOrderDTO> orderDTO = new List<UserOrderDTO>();
            foreach (var order in orders)
            {
                orderDTO.Add(order.ToUserOrderDTO());
            }
            return orderDTO;
        }

        public async Task<List<StockDetailsDTO>> GetAllStock(string Name)
        {
            List<StockDetails> stocks = await _usersRepository.GetAllStocks(Name);
            List<StockDetailsDTO> stocksDTO = new List<StockDetailsDTO>();
            foreach (var stock in stocks)
            {
                stocksDTO.Add(stock.ToStockDetailsDTO());
            }
            return stocksDTO;
        }

        public async Task<UserDetailsDTO> AddUser(UserDetailsDTO user)
        {

            UserDetails newUser = await _usersRepository.AddUser(user.ToUserDetails());
            return newUser.ToUserDetailDTO();
        }

        public async Task<UserOrderDTO> AddUserOrder(string Name, UserOrderDTO orders)
        {
            if (orders.OrderType == "b")
            {
                await _usersRepository.AlterUserMoney(Name, orders.Price,orders.Amount,orders.StockTicker);
                var newOrder1 = await _usersRepository.CreateNewOrder(orders.ToUserOrders(Name));
                return newOrder1.ToUserOrderDTO();
            }
            await _usersRepository.AlterUserMoney(Name, -orders.Price,orders.Amount,orders.StockTicker);
            var newOrder = await _usersRepository.CreateNewOrder(orders.ToUserOrders(Name));
            return newOrder.ToUserOrderDTO();
        }

        public async Task<UserDetailsDTO?> AddFunds(string Name, decimal Amount)
        {
            UserDetails? user = await _usersRepository.GetUser(Name);
            if (user == null)
            {
                return null;
            }
            UserDetails? userDTO = await _usersRepository.AddFunds(Name, Amount);
            if (userDTO == null)
            {
                return null;
            }
            return userDTO.ToUserDetailDTO();
        }
    }
}
