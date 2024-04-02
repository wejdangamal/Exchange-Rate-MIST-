using Exchange_Rates.Models;
using Exchange_Rates.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Exchange_Rates.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class HomeController : Controller
    {
        private readonly IConsuming connect;


        public HomeController(IConsuming _connect)
        {
            connect = _connect;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var rates = await connect.GetToken();
                return View(rates);
            }
            catch (Exception ex)
            {
                var x = new ErrorResponse
                {
                    error_type = ex.Message
                };
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> getCurrencyRate([FromQuery] string currencyName)
        {
            var rate = await connect.GetExchangeInfo(currencyName);
            return Json(rate);
        }
        [HttpPost]
        public async Task<IActionResult> GetUpdatedInfo([FromBody] string[] currencyName)
        {
            if (currencyName != null)
            {
                var res = await connect.GetExchangeInfoTest(currencyName);
                return Json(res);
            }
            return Json("notFound");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
