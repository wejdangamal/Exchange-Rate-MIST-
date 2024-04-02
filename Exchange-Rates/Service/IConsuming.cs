using Exchange_Rates.Models;

namespace Exchange_Rates.Service
{
    public interface IConsuming
    {
        Task<List<OutData>> GetToken();
        Task<decimal> GetExchangeInfo(string name);
        Task<List<OutData>> GetExchangeInfoTest(string[] name);
        Task<decimal> ConversionRates(string from,string to, decimal amount);
    }
}
