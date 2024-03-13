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
        ViewBag.path = HttpContext.Request.PathBase + HttpContext.Request.Path + HttpContext.Request.QueryString;
        return View(data);
    }
    public async Task<ActionResult<FarmVm>> CreateOrEditRoofTop(int id, CancellationToken cancellationToken)
    {
        ViewBag.HouseOwners = houseOwnerRepository.Dropdown();
        ViewBag.path = HttpContext.Request.PathBase + HttpContext.Request.Path + HttpContext.Request.QueryString;
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
    public async Task<ActionResult<FarmVm>> CreateOrEditRoofTop(int id, FarmVm farmVm,IFormFile photo1, IFormFile photo2, IFormFile photo3, CancellationToken cancellation)
    {

        if (id == 0)
        {
            if (photo1 != null  && photo1.Length > 0)
            {     
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/img/", photo1.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    photo1.CopyTo(stream);
                }
                farmVm.Image1 = $"{photo1.FileName}";
            }

            if (photo2 != null && photo2.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/img/", photo2.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    photo2.CopyTo(stream);
                }
                farmVm.Image2 = $"{photo2.FileName}";
            }

            if (photo3 != null && photo3.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/img/", photo3.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    photo3.CopyTo(stream);
                }
                farmVm.Image3 = $"{photo3.FileName}";
            }
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
