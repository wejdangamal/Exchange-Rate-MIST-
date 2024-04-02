using Exchange_Rates.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Exchange_Rates.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class ConversionController : Controller
    {
        private readonly IConsuming consuming;

        public ConversionController(IConsuming consuming)
        {
            this.consuming = consuming;
        }
        [HttpGet]
        public async Task<IActionResult> GetConversion()
        {
           var list = await ExchangeRatesBag();
            ViewBag.fromCurrencyBag = list;
            var list2 = await ExchangeRatesBag(); 
            list2[4].Selected = true;
            ViewBag.toCurrencyBag = list2;

            return View("Conversion");
        }
        private async Task<List<SelectListItem>> ExchangeRatesBag()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var exchangeRates = await consuming.GetToken();
            foreach (var exchangeRate in exchangeRates)
            {
                list.Add(new SelectListItem(exchangeRate.Currency, exchangeRate.Currency.ToString()));
            }
            return list;
        }
        [HttpGet]
        public async Task<IActionResult> Conversion(string from = "USD",string to ="EGP",decimal amount =1)
        {
            decimal result =await consuming.ConversionRates(from,to, amount);

            return Json(result);
        }
    }
}