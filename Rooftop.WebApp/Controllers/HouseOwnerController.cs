using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.RepositoryService;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.Controllers;

public class HouseOwnerController : Controller
{
    private readonly IHouseOwnerRepository houseOwnerRepository;



    public HouseOwnerController(IHouseOwnerRepository houseOwnerRepository)
    {
        this.houseOwnerRepository = houseOwnerRepository;

    }
    public async Task<IActionResult> Index()
    {
        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null)
        {
            var data = await houseOwnerRepository.GetAllAsync();
            return View(data);
        }


        return RedirectToAction("Login", "Admin");
    }

    public async Task<ActionResult<HouseOwnerVm>> CreateOrEditHouseOwner(int id, CancellationToken cancellationToken)
    {



        if (id == 0)
        {
            return View(new HouseOwnerVm());
        }
        else
        {
            var data = await houseOwnerRepository.GetByIdAsync(id, cancellationToken);
            data.Password = null;
            return View(data);
        }



    }
    [HttpPost]
    public async Task<ActionResult<HouseOwnerVm>> CreateOrEditHouseOwner(int id, HouseOwnerVm houseOwnerVm, CancellationToken cancellation)
    {
      
            if (id == 0)
            {
               
                await houseOwnerRepository.InsertAsync(houseOwnerVm, cancellation);
                return RedirectToAction("Index");

            }
            else
            {
                await houseOwnerRepository.UpdateAsync(id, houseOwnerVm, cancellation);
                return RedirectToAction("Index");
            }

       

    }
    public async Task<ActionResult<HouseOwnerVm>> Delete(int id, CancellationToken cancellation)
    {
       
        var user = HttpContext.Session.GetString("adminEmail");  
        if (user != null )
        {

            if (id != 0)
            {
                await houseOwnerRepository.DeleteAsync(id, cancellation);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }


        return RedirectToAction("Login", "HouseOwner");


    }
    public async Task<ActionResult<HouseOwnerVm>> Details(int id, CancellationToken cancellation)
    {
        var HO = HttpContext.Session.GetInt32("HouseOwnerId");
       
        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null || HO != 0)
        {
            if (HO != 0 && HO != null)
            {
                id = (int)HO;
            }
            return View(await houseOwnerRepository.GetByIdAsync(id, cancellation));

        }


        return RedirectToAction("Login", "Admin");

    }


    public IActionResult Login()
    {

        HttpContext.Session.GetInt32("HouseOwnerId");

        return View();

    }
    [HttpPost]
    public IActionResult Login(HouseOwners houseOwners)
    {
        var HOId = houseOwnerRepository.CurrentHouseOwner(houseOwners);
        if (HOId != 0)
        {

            if (houseOwners != null)
            {
                HttpContext.Session.SetInt32("HouseOwnerId", HOId);
                HttpContext.Session.Remove("adminEmail");
            }


            return RedirectToAction("Index","Home");
        }
        else
        {
            return View();
        }

    }
    public IActionResult Logout()
    {
        if (HttpContext.Session.GetInt32("HouseOwnerId") != null)
        {
            HttpContext.Session.Remove("HouseOwnerId");
            return RedirectToAction("Index", "Home");
        }
        return View();

    }
}