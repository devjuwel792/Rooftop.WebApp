using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.RepositoryService;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository userRepository;

    public UserController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public async Task<IActionResult> Index()
    {

        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null)
        {

            var data = await userRepository.GetAllAsync();

            return View(data);
        }


        return RedirectToAction("Login", "Admin");

    }
    public async Task<ActionResult<UserVm>> CreateOrEditUser(int id, CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return View(new UserVm());

        }
        else
        {
            var data = await userRepository.GetByIdAsync(id, cancellationToken);
            data.Password = null;
            return View(data);
        }

    }
    [HttpPost]
    public async Task<ActionResult<UserVm>> CreateOrEditUser(int id, UserVm userVm, CancellationToken cancellation)
    {
        if (id == 0)
        {
            await userRepository.InsertAsync(userVm, cancellation);
            return RedirectToAction("Index", "Farm");

        }
        else
        {
            await userRepository.UpdateAsync(id, userVm, cancellation);
            return RedirectToAction("Index", "Farm");
        }
    }
    public async Task<ActionResult<UserVm>> Delete(int id, CancellationToken cancellation)
    {

        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null)
        {
            if (id != 0)
            {
                await userRepository.DeleteAsync(id, cancellation);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }


        return RedirectToAction("Login", "Admin");

    }
    public IActionResult Login()
    {

        HttpContext.Session.GetInt32("UserId");

        return View();

    }
    [HttpPost]
    public IActionResult Login(User user)
    {
        var UId = userRepository.CurrentUser(user);
        if (UId != 0)
        {

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", UId);
                HttpContext.Session.Remove("adminEmail");
                HttpContext.Session.Remove("HouseOwnerId");
            }


            return RedirectToAction("Index", "Farm");
        }
        else
        {
            return View();
        }

    }
    public IActionResult Logout()
    {
        if (HttpContext.Session.GetInt32("UserId") != null && HttpContext.Session.GetInt32("UserId") != 0)
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Home");
        }
        return View();

    }
}