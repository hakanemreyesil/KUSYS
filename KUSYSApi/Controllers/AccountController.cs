using Microsoft.AspNetCore.Mvc;

namespace KUSYSApi.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
