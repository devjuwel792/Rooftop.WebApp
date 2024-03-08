using Microsoft.AspNetCore.Mvc;
using Mono.TextTemplating;
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
        var data =await userRepository.GetAllAsync();
       
        return View(data);
    }
    public async Task<ActionResult<UserVm>> CreateOrEditUser(int id,CancellationToken cancellationToken)
    {
        ViewBag.path = HttpContext.Request.PathBase + HttpContext.Request.Path + HttpContext.Request.QueryString;
        if (id == 0)
        {
            return View(new UserVm());

        }
        else
        {
            return View(await userRepository.GetByIdAsync(id, cancellationToken));
        }
        
    }
    [HttpPost]
    public async Task<ActionResult<UserVm>> CreateOrEditUser(int id, UserVm userVm, CancellationToken cancellation)
    {
        if (id == 0)
        {
            await userRepository.InsertAsync(userVm, cancellation);
            return RedirectToAction("Index");

        }
        else
        {
            await userRepository.UpdateAsync(id, userVm, cancellation);
            return RedirectToAction("Index");
        }
    }
    public async Task<ActionResult<UserVm>> Delete(int id, CancellationToken cancellation)
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

}
