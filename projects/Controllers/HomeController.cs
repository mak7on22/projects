using Microsoft.AspNetCore.Mvc;

namespace projects.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/");
        }
    }
}
