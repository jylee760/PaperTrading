using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using PaperTrading.Entities.MVCRepositories;
using PaperTrading.Models.DTO;
using PaperTrading.Models.ui;
using PaperTradingApi.Models.DTO;

namespace PaperTrading.Controllers.MVCControllers
{
    public class HomeController: Controller
    {
        private readonly IDbService _apiService;
        private readonly IConfiguration _configuration;
        private readonly IFinnhubService _finnhubService;
        public HomeController(IDbService service,IConfiguration configuration,IFinnhubService finnhubService)
        {
            _apiService = service;
            _configuration = configuration;
            _finnhubService = finnhubService;
        }
        [HttpPost]
        public async Task<IActionResult> SearchStock(string stockTicker){
            StockProfile? stockProfile = await _finnhubService.getStockProfile(stockTicker, _configuration["FinnhubToken"]);
            if (stockProfile.IsEmpty())
            {
                ViewBag.BadSearch = true;
                return View("StockSearch");
            }
            Quote? stockQuote = await _finnhubService.getQuote(stockTicker, _configuration["FinnhubToken"]);
            ViewBag.StockProfile = stockProfile;
            ViewBag.StockQuote = stockQuote;
            var userDetails = await _apiService.getUserDetails(HttpContext.Session.GetString("User"), HttpContext.Session.GetString("Token"));
            ViewBag.CurrentMoney = userDetails.CurrentMoney;
            ViewBag.Token = _configuration["FinnhubToken"];
            return View("StockSearch");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Add_Funds(UserDetailsAddFundsDTO dto)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Login");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add_Funds");
            }
            await _apiService.addFunds(HttpContext.Session.GetString("User"), HttpContext.Session.GetString("Token"), dto);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Add_Funds()
        {
            if(String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Login");
            }
            return View("Add");
        }
        [HttpPost]
        public async Task<IActionResult> Order(OrderDTO order,string OrderType,int MaxAmount)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Login");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var userDetails = await _apiService.getUserDetails(HttpContext.Session.GetString("User"), HttpContext.Session.GetString("Token"));
            if (OrderType.Equals("s") && MaxAmount < order.Amount)
            {
                return RedirectToAction("Index");
            }
            if (OrderType.Equals("b") && (order.Amount * order.Price) > userDetails.CurrentMoney)
            {
                return RedirectToAction("Index");
            }
            var userOrder = new UserOrderDTO { 
                OrderType = OrderType,
                Amount = order.Amount,
                Price = order.Price,
                StockTicker = order.StockTicker,
                Timestamp = DateTime.Now
            };
            await _apiService.placeOrder(HttpContext.Session.GetString("User"), HttpContext.Session.GetString("Token"), userOrder);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("/orders")]
        public async Task<IActionResult> Orders()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Login");
            }
            var orders = await _apiService.getOrders(HttpContext.Session.GetString("User"), HttpContext.Session.GetString("Token"));
            ViewBag.Orders = orders;
            return View("Orders");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                LoginDTO getTokenApi = new LoginDTO()
                {
                    Username = _configuration["UsernameAPI"],
                    Password = _configuration["PasswordAPI"]
                };
                var token = await _apiService.Login(getTokenApi);
                HttpContext.Session.SetString("Token", token);
            }
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserDetailsDTO details)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                LoginDTO getTokenApi = new LoginDTO()
                {
                    Username = _configuration["UsernameAPI"],
                    Password = _configuration["PasswordAPI"]
                };
                var token = await _apiService.Login(getTokenApi);
                HttpContext.Session.SetString("Token", token);
            }
            var response = await _apiService.getUserDetails(details.UserName, HttpContext.Session.GetString("Token"));
            if (response == null) {
                var newUser = await _apiService.register(details, HttpContext.Session.GetString("Token"));
                HttpContext.Session.SetString("User", newUser.UserName);
                return RedirectToAction("Index");
            }
            ViewBag.UserExists = true;
            return View("Register");
        }
        [HttpGet]
        [Route("/")]
        [Route("/home")]
        [Route("/swagger")]
        public async Task<IActionResult> Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Login");
            }
            var user = await _apiService.getUserDetails(HttpContext.Session.GetString("User"), HttpContext.Session.GetString("Token"));
            var stocksList = await _apiService.retrieveStocks(HttpContext.Session.GetString("User"), HttpContext.Session.GetString("Token"));
            Dictionary<String, decimal?> quoteList = new Dictionary<String, decimal?>();
            foreach(var stock in stocksList)
            {
                var response = await _finnhubService.getQuote(stock.StockTicker, _configuration["FinnhubToken"]);
                quoteList[stock.StockTicker] = response.c;
            }
            ViewBag.QuoteList = quoteList;
            ViewBag.UserDetails = user;
            ViewBag.Stocks = stocksList;
            ViewBag.Token = _configuration["FinnhubToken"];
            return View();
        }
        [HttpGet]
        [Route("/login")]
        public async Task<IActionResult> Login()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                LoginDTO getTokenApi = new LoginDTO
                {
                    Username = _configuration["UsernameAPI"],
                    Password = _configuration["PasswordAPI"]
                };
                var token = await _apiService.Login(getTokenApi);
                HttpContext.Session.SetString("Token", token);
            }
            return View("Login");
        }
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(UserLoginDTO login)
        {
            var user = await _apiService.ValidateUser(login, HttpContext.Session.GetString("Token"));
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login");
            }
            else
            {
                HttpContext.Session.SetString("User", user);
                return RedirectToAction("Index");
            }
        }
    }
}
