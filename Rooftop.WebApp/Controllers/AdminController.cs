using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.RepositoryService;
namespace Rooftop.WebApp.Controllers;

public class AdminController : Controller
{
    private readonly IAdminRepository adminRepository;

    public AdminController(IAdminRepository adminRepository)
    {
        this.adminRepository = adminRepository;
    }

    public IActionResult Index()
    {
        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null)
        {
            ViewBag.User = user;
            return View(new Admin());
        }
        else
        {

            return RedirectToAction("Login");
        }

    }
    public IActionResult Login()
    {

        HttpContext.Session.GetString("adminEmail");

        return View();

    }
    [HttpPost]
    public IActionResult Login(Admin admin)
    {
        if (adminRepository.Current(admin))
        {

            if (admin != null)
            {
                HttpContext.Session.SetString("adminEmail", admin.Email);

            }


            return RedirectToAction("Index");
        }
        else
        {
            return View();
        }

    }
    public IActionResult Logout()
    {
        if (HttpContext.Session.GetString("adminEmail") != null)
        {
            HttpContext.Session.Remove("adminEmail");
            return RedirectToAction("Index", "Home");
        }
        return View();

    }


}
