
using PaperTrading.Models.DTO;
using PaperTrading.Models.ui;
using PaperTradingApi.Models.DTO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PaperTrading.Entities.MVCRepositories
{
    public class ApiService : IDbService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        private async Task<string?> getApi(String url,String jwt)
        {
            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }
        public async Task<UserDetailsDTO> addFunds(string user, string jwt, UserDetailsAddFundsDTO funds)
        {
            var client = httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(funds), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var response = await client.PatchAsync($"http://localhost:5183/api/persons/addfunds/{user}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseDetail = await response.Content.ReadAsStringAsync();
                UserDetailsDTO userDetails = JsonSerializer.Deserialize<UserDetailsDTO>(responseDetail, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return userDetails;
            }
            return null;
        }

        public async Task<List<UserOrderDTO>> getOrders(string user, string jwt)
        {
            var response = await getApi($"http://localhost:5183/api/persons/history/{user}",jwt);
            if(response != null)
            {
                List<UserOrderDTO> ordersList = JsonSerializer.Deserialize<List<UserOrderDTO>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).OrderByDescending(order => order.Timestamp).ToList();
                return ordersList;
            }
            return null;
        }

        public async Task<UserDetailsDTO> getUserDetails(string user, string jwt)
        {
            var response = await getApi($"http://localhost:5183/api/persons/{user}", jwt);
            if (response != null)
            {
                UserDetailsDTO userDetails = JsonSerializer.Deserialize<UserDetailsDTO>(response,new JsonSerializerOptions() { PropertyNameCaseInsensitive=true});
                return userDetails;
            }
            return null;
        }

        public async Task<string?> Login(LoginDTO login)
        {
            var client = httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5183/api/auth/login", content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<LoginResponseDTO>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).jwtToken;
                return token;
            }
            else
            {
                return "";
            }
        }

        public async Task<UserOrderDTO> placeOrder(string user, string jwt, UserOrderDTO order)
        {
            var client = httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var response = await client.PostAsync($"http://localhost:5183/api/persons/create/{user}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseOrder = await response.Content.ReadAsStringAsync();
                UserOrderDTO orderDTO = JsonSerializer.Deserialize<UserOrderDTO>(responseOrder, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return orderDTO;
            }
            return null;
        }

        public async Task<UserDetailsDTO> register(UserDetailsDTO details, string jwt)
        {
            var client = httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(details), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var response = await client.PostAsync($"http://localhost:5183/api/persons", content);
            if (response.IsSuccessStatusCode)
            {
                var responseDetail = await response.Content.ReadAsStringAsync();
                UserDetailsDTO userDetails = JsonSerializer.Deserialize<UserDetailsDTO>(responseDetail, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return userDetails;
            }
            return null;
        }

        public async Task<List<StocksHeld>?> retrieveStocks(string user, string jwt)
        {
            var response = await getApi($"http://localhost:5183/api/persons/allstocks/{user}", jwt);
            if(response != null)
            {
                List<StocksHeld> stocksHeldList = JsonSerializer.Deserialize<List<StocksHeld>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return stocksHeldList;
            }
            return null;
        }

        public async Task<string?> ValidateUser(UserLoginDTO login, string jwt)
        {
            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var response = await client.GetAsync($"http://localhost:5183/api/persons/{login.UserName}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var password = JsonSerializer.Deserialize<UserDetailsDTO>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).Password;
                if (!password.Equals(login.Password))
                {
                    return null;
                }
                return login.UserName;
            }
            return null;
        }
    }
}
