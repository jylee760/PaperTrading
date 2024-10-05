namespace PaperTrading.Models.ui
{
    public class StockProfile
    {
        public string? country { get; set; }
        public string? currency { get; set; }
        public string? exchange { get; set; }
        public string? finnhubIndustry { get; set; }
        public string? logo { get; set; }
        public string? name { get; set; }
        public decimal? marketCapitalization { get; set; }
        public string? ticker { get; set; }
        public string? weburl { get; set; }
        public decimal? shareOutstanding { get; set; }
        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(name);
        }
    }
}
