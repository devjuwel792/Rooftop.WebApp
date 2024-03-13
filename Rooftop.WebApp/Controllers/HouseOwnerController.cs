using Microsoft.AspNetCore.Mvc;
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
        if (user != null)
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


        return RedirectToAction("Login", "Admin");


    }
    public async Task<ActionResult<HouseOwnerVm>> Details(int id, CancellationToken cancellation)
    {

        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null)
        {
            return View(await houseOwnerRepository.GetByIdAsync(id, cancellation));

        }


        return RedirectToAction("Login", "Admin");

    }

}
