namespace Exchange_Rates.Models
{
    public class ErrorResponse
    {
        public string result { get; set; } = "error";
        public string error_type { get; set; }
    }
}
