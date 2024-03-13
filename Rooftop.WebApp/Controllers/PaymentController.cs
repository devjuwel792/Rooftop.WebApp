using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.Models;
using System.Collections.Generic;

namespace Rooftop.WebApp.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
