using Microsoft.AspNetCore.Mvc;

using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.RepositoryService;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.Controllers;

public class HouseOwnerController : Controller
{
    private readonly IHouseOwnerRepository houseOwnerRepository;

    public ApplicationDbContext A { get; }

    public HouseOwnerController(IHouseOwnerRepository houseOwnerRepository,ApplicationDbContext a)
    {
        this.houseOwnerRepository = houseOwnerRepository;
        A = a;
    }
    public async Task<IActionResult> Index()
    {
        var data = await houseOwnerRepository.GetAllAsync();
        ViewBag.juwel = "my name is juwel";
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
            return View(await houseOwnerRepository.GetByIdAsync(id, cancellationToken));
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
