using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.RepositoryService;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.Controllers;

public class FarmController : Controller
{
    private readonly IFarmRepository farmRepository;
    private readonly IHouseOwnerRepository houseOwnerRepository;

    public FarmController(IFarmRepository farmRepository, IHouseOwnerRepository houseOwnerRepository)
    {
        this.farmRepository = farmRepository;
        this.houseOwnerRepository = houseOwnerRepository;
    }
    public async Task<IActionResult> Index()
    {
        var data = await farmRepository.GetAllAsync(x => x.HouseOwners);
        ViewBag.path = HttpContext.Request.PathBase + HttpContext.Request.Path + HttpContext.Request.QueryString;
        return View(data);
    }
    public async Task<ActionResult<FarmVm>> CreateOrEditRoofTop(int id, CancellationToken cancellationToken)
    {
        var HO = HttpContext.Session.GetInt32("HouseOwnerId");
        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null || (HO != null && HO != 0))
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

        return RedirectToAction("Login", "HouseOwner");


    }
    [HttpPost]
    public async Task<ActionResult<FarmVm>> CreateOrEditRoofTop(int id, FarmVm farmVm, IFormFile photo1, IFormFile photo2, IFormFile photo3, CancellationToken cancellation)
    {
        var HO = HttpContext.Session.GetInt32("HouseOwnerId");
        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null || (HO != null && HO != 0))
        {
            if (HO != null && HO != 0)
            {
                farmVm.HouseOwnersId = (int)HO;
            }
            if (id == 0)
            {
                if (photo1 != null && photo1.Length > 0)
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

        return RedirectToAction("Login", "HouseOwner");

    }
    public async Task<ActionResult<FarmVm>> Delete(int id, CancellationToken cancellation)
    {
        var HO = HttpContext.Session.GetInt32("HouseOwnerId");
        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null || (HO != null && HO != 0))
        {
            var RHO = await farmRepository.GetByIdAsync(id, cancellation);
            if (RHO.HouseOwnersId == HO || user != null)
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
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }
        return RedirectToAction("Login", "HouseOwner");
    }
    public async Task<ActionResult<FarmVm>> Details(int id, CancellationToken cancellation)
    {
        var HO = HttpContext.Session.GetInt32("HouseOwnerId");
        var user = HttpContext.Session.GetString("adminEmail");
        var UId = HttpContext.Session.GetInt32("UserId");
        if (user != null || (HO != null && HO != 0)|| (UId != null && UId != 0))
        { 
         return View(await farmRepository.GetByIdAsync(id, cancellation));
        }
        return RedirectToAction("Login", "User");
    }

    public ActionResult test(int id)
    {
        var HO = HttpContext.Session.GetInt32("HouseOwnerId");
        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null || (HO != null && HO != 0))
        {
            return Json(farmRepository.GetHouseOwnerFarmAsync(id));
        }
        return RedirectToAction("Login", "Home");
    }


}
