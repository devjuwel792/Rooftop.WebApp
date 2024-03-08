using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.RepositoryService;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.Controllers;

public class FarmController : Controller
{
    private readonly IFarmRepository farmRepository;
    private readonly IHouseOwnerRepository houseOwnerRepository;

    public FarmController(IFarmRepository farmRepository,IHouseOwnerRepository houseOwnerRepository)
    {
        this.farmRepository = farmRepository;
        this.houseOwnerRepository = houseOwnerRepository;
    }
    public async Task<IActionResult> Index()
    {
        var data = await farmRepository.GetAllAsync(x=>x.HouseOwners);
        
        return View(data);
    }
    public async Task<ActionResult<FarmVm>> CreateOrEditRoofTop(int id, CancellationToken cancellationToken)
    {
        ViewBag.HouseOwners = houseOwnerRepository.Dropdown();

        if (id == 0)
        {
            return View(new FarmVm());
        }
        else
        {
            return View(await farmRepository.GetByIdAsync(id, cancellationToken));
        }

    }
    [HttpPost]
    public async Task<ActionResult<FarmVm>> CreateOrEditRoofTop(int id, FarmVm farmVm,IFormFile photo,  CancellationToken cancellation)
    {
        if (id == 0)
        {
            await farmRepository.InsertAsync(farmVm, cancellation);
            return RedirectToAction("Index");

        }
        else
        {
            await farmRepository.UpdateAsync(id, farmVm, cancellation);
            return RedirectToAction("Index");
        }
    }
    public async Task<ActionResult<FarmVm>> Delete(int id, CancellationToken cancellation)
    {

        if (id != 0)
        {
            await farmRepository.DeleteAsync(id, cancellation);
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return RedirectToAction(nameof(Index));
        }
    }
    public async Task<ActionResult<FarmVm>> Details(int id, CancellationToken cancellation)
    {
        return View(await farmRepository.GetByIdAsync(id, cancellation));
    }

    public ActionResult test(int id)
    {
        return Json(farmRepository.GetHouseOwnerFarmAsync(id));
    }


}
