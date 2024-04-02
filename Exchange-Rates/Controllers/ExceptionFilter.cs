using Exchange_Rates.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Exchange_Rates.Controllers
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ViewResult v =new ViewResult();           
            v.ViewName = "Error";
            context.Result =v;
            context.ExceptionHandled = true;
        }
    }
}
