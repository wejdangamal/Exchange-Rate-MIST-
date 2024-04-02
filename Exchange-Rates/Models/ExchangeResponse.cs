namespace Exchange_Rates.Models
{
    public class ExchangeResponse
    {
        public string Result { get; set; }
        public string Documentation { get; set; }
        public string terms_of_use { get; set; }
        public long time_last_update_unix { get; set; }
        public string time_last_update_utc { get; set; }
        public long time_next_update_unix { get; set; }
        public string time_next_update_utc { get; set; }
        public string base_code { get; set; }
        public Dictionary<string, decimal> conversion_rates { get; set; }
    }
}
