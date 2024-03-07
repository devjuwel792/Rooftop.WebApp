using Microsoft.AspNetCore.Mvc;

namespace Rooftop.WebApp.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
   

}
