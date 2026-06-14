using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GoBL.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool postojiRaspis = ProvjeriRaspis();
            ViewData["PostojiRaspisTurnira"] = postojiRaspis;
            base.OnActionExecuting(context);
        }

        private bool ProvjeriRaspis()
        {
            // Simulirano: možeš povezati sa bazom, fajlom, itd.
            return true; 
        }
    }
}
