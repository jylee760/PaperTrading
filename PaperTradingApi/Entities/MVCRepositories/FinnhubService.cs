using PaperTrading.Models.ui;
using System.Text.Json;

namespace PaperTrading.Entities.MVCRepositories
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public FinnhubService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        private async  Task<string?> getApi(String url)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }
        public async Task<Quote?> getQuote(string stockTicker, string Token)
        {
            String url = $"https://finnhub.io/api/v1/quote?symbol={stockTicker.ToUpper()}&token={Token}";
            var quoteResponse = await getApi(url);
            if(quoteResponse != null)
            {
                Quote quote =JsonSerializer.Deserialize<Quote>(quoteResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return quote;
            }
            return null;
        }

        public async Task<StockProfile?> getStockProfile(string stockTicker, string Token)
        {
            String url = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockTicker.ToUpper()}&token={Token}";
            var stockResponse = await getApi(url);
            if (stockResponse!=null)
            {
                StockProfile profile = JsonSerializer.Deserialize<StockProfile>(stockResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return profile;
            }
            return null;
        }
    }
}
