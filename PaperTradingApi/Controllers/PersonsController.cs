using Microsoft.AspNetCore.Mvc;
using PaperTradingApi.Entities;
using PaperTradingApi.Models.DTO;

namespace PaperTradingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController:ControllerBase
    {
        private readonly IUsersService _usersService;
        public PersonsController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetUser([FromRoute] String name)
        {
            UserDetailsDTO? person = await _usersService.GetUser(name);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }
        [HttpGet]
        [Route("stock/{name}/{stock}")]
        public async Task<IActionResult> GetUserStock([FromRoute] String name, [FromRoute] String stock)
        {
            StockDetailsDTO? userStock = await _usersService.GetUserStock(name, stock);
            if (userStock == null)
            {
                return NotFound();
            }
            return Ok(userStock);
        }
        [HttpGet]
        [Route("order/{name}/{timestamp}")]
        public async Task<IActionResult> GetUserOrder([FromRoute] String name, [FromRoute] DateTime timestamp)
        {
            UserOrderDTO? userOrder = await _usersService.GetUserOrder(name, timestamp);
            if (userOrder == null)
            {
                return NotFound();
            }
            return Ok(userOrder);
        }
        [HttpGet]
        [Route("history/{name}")]
        public async Task<IActionResult> GetUserOrderHistory([FromRoute] String name)
        {
            List<UserOrderDTO> orders = await _usersService.GetUserHistory(name);
            return Ok(orders);
        }
        [HttpGet]
        [Route("allstocks/{name}")]
        public async Task<IActionResult> GetAllStocks([FromRoute] String name)
        {
            List<StockDetailsDTO> stocks = await _usersService.GetAllStock(name);
            return Ok(stocks);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] UserDetailsDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var check = await _usersService.GetUser(user.UserName);
            if (check != null)
            {
                return Conflict("User already exists");
            }
            UserDetailsDTO addedUser = await _usersService.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { name = addedUser.UserName }, addedUser);
        }
        [HttpPost]
        [Route("create/{user}")]
        public async Task<IActionResult> CreateNewOrder(String user, [FromBody] UserOrderDTO order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var account = await _usersService.GetUser(user);
            if (account == null)
            {
                return NotFound();
            }
            if (account.CurrentMoney - order.Price < 0 && order.OrderType == "b")
            {
                return BadRequest("Insufficient Funds");
            }
            var stock = await _usersService.GetUserStock(user, order.StockTicker);
            if ((stock == null || stock.Amount < order.Amount) && order.OrderType == "s")
            {
                return BadRequest("Insufficient Stock Holdings");
            }
            var check = await _usersService.GetUserOrder(user, order.Timestamp);
            if (check != null)
            {
                return BadRequest("How you execute multiple orders at the same time bruh ts not allowed");
            }
            UserOrderDTO orderR = await _usersService.AddUserOrder(user, order);
            return CreatedAtAction(nameof(GetUserOrder), new { name = user, timestamp = orderR.Timestamp.ToString("yyyy-MM-ddTHH:mm:ss") }, orderR);
        }
        [HttpPatch]
        [Route("addfunds/{user}")]
        public async Task<IActionResult> AddFunds(String user, [FromBody] UserDetailsAddFundsDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserDetailsDTO? dtoChanged = await _usersService.AddFunds(user, dto.Amount);
            if (dtoChanged == null)
            {
                return NotFound();
            }
            return Ok(dtoChanged);
        }

    }
}
