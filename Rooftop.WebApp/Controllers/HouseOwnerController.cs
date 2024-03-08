using Microsoft.AspNetCore.Mvc;

using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.RepositoryService;
using Rooftop.WebApp.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        var data = await houseOwnerRepository.GetAllAsync();
        
        return View(data);
    }
    public async Task<ActionResult<HouseOwnerVm>> CreateOrEditRoofTop(int id, CancellationToken cancellationToken)
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
    public async Task<ActionResult<HouseOwnerVm>> CreateOrEditRoofTop(int id, HouseOwnerVm houseOwnerVm, CancellationToken cancellation)
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
    public async Task<ActionResult<HouseOwnerVm>> Details(int id, CancellationToken cancellation)
    {      
        return View(await houseOwnerRepository.GetByIdAsync(id,cancellation));
    }
    
}
