using Exchange_Rates.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Exchange_Rates.Service
{
    public class Consuming : IConsuming
    {
        private readonly HttpClient _client;
        private readonly IHttpClientFactory _factory;
        public Consuming(IHttpClientFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient("ExchangeRate");
        }
        public async Task<decimal> ConversionRates(string from, string to,decimal amount)
        {
            Uri convertURL = new Uri(@$"https://v6.exchangerate-api.com/v6/aae5451edb446b16928b6338/pair/{from}/{to}");
            var req = await _client.GetAsync(convertURL);
            if (req.IsSuccessStatusCode)
            {
                var res = await req.Content.ReadAsStringAsync();
                var desc = JsonConvert.DeserializeObject<ConversionResponse>(res);
                var conversionResult = Math.Round(desc.conversion_rate * amount,2);
                return conversionResult;
            }          
            return 0;
        }

        public async Task<decimal> GetExchangeInfo(string name)
        {
            if(name !=null)
            {
                var exchangeCurrencies = await GetToken();
                decimal rate = exchangeCurrencies.FirstOrDefault(x => x.Currency == name)?.Rate ?? 0;
                return rate;
            }
            return 0;
        }

        public async Task<List<OutData>> GetExchangeInfoTest(string[] name)
        {
            List<OutData> outData = new List<OutData>();
            if (name != null)
            {
                
                var getLists = await GetToken();
                foreach(string na in name)
                {
                    var result = getLists.Where(x => x.Currency == na).FirstOrDefault();
                    outData.Add(result);
                }

            }
            return outData;
        }

        public async Task<List<OutData>> GetToken()
        {
            IEnumerable<OutData> list = null;
            var req = await _client.GetAsync(_client.BaseAddress);
            if (req.IsSuccessStatusCode)
            {
                var res = await req.Content.ReadAsStringAsync();
                var desc = JsonConvert.DeserializeObject<ExchangeResponse>(res);
                list = desc.conversion_rates.Select(x => new OutData
                {
                    Currency = x.Key,
                    Rate = x.Value
                }).ToList();
            }
            else
            {
                list = Enumerable.Empty<OutData>();
            }

            return list.ToList();
        }
    }
}
