using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.RepositoryService;
using Rooftop.WebApp.ViewModel;
using System.Collections.Immutable;
using System.Threading;

namespace Rooftop.WebApp.Controllers;

public class PaymentController : Controller
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IUserRepository userRepository;

    public PaymentController(IPaymentRepository paymentRepository, IUserRepository userRepository)
    {
        this.paymentRepository = paymentRepository;
        this.userRepository = userRepository;
    }
    public async Task<IActionResult> Index()
    {

        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null)
        {
            ViewBag.path = HttpContext.Request.PathBase + HttpContext.Request.Path + HttpContext.Request.QueryString;
            var data = await paymentRepository.GetAllAsync(x => x.User, x => x.farm, x => x.farm.HouseOwners);
            //var result = data.Where(x=>x.IsPaymentConfirmed).ToList();
            //var result = from obj in data
            //             where (obj.IsPaymentConfirmed)
            //             select obj;
            var result = data.AsQueryable().Where(x => x.IsPaymentConfirmed).ToList();
            result.Reverse();
            return View(result);
        }
        return RedirectToAction("Login", "Admin");

    }

    public async Task<ActionResult<PaymentVm>> Pending()
    {
        ViewBag.path = HttpContext.Request.PathBase + HttpContext.Request.Path + HttpContext.Request.QueryString;
        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null)
        {
            var data = await paymentRepository.GetAllAsync(x => x.User, x => x.farm, x => x.farm.HouseOwners);
            var result = data.AsQueryable().Where(x => !x.IsPaymentConfirmed).ToList();
            result.Reverse();
            return View(result);
        }
        return RedirectToAction("Login", "Admin");

    }

    [HttpPost]
    public async Task<ActionResult<PaymentVm>> Pending(int id, CancellationToken cancellationToken)
    {
        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null)
        {
            var payment = await paymentRepository.GetByIdAsync(id, cancellationToken);
            payment.IsPaymentConfirmed = true;

            await paymentRepository.UpdateAsync(id, payment, cancellationToken);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Login", "Admin");

    }
    public IActionResult BookNow(int id)
    {
        HttpContext.Session.SetInt32("FarmId", id);
        return RedirectToAction("paymentSuccess");
    }
    public async Task<IActionResult> PaymentSuccess()

    {
        var FarmId = HttpContext.Session.GetInt32("FarmId");
        var userId = HttpContext.Session.GetInt32("UserId");
        if (FarmId != 0 && FarmId != null && userId != 0 && userId != null)
        {
            return View();
        }
        if (userId == 0 || userId == null)
        {
            return RedirectToAction("Login", "User");
        }
        return RedirectToAction("Index", "Farm");


    }
    [HttpPost]
    public async Task<IActionResult> PaymentSuccess(PaymentVm payment, CancellationToken cancellationToken)
    {
       
            var userId = HttpContext.Session.GetInt32("UserId");
            var FarmId = HttpContext.Session.GetInt32("FarmId");

            if (FarmId != 0 && FarmId != null && userId != 0 && userId != null)
            {
                var user = await userRepository.GetByIdAsync((int)userId, cancellationToken);
                payment.IsPaymentConfirmed = false;
                payment.UserId = user.Id;
                payment.Email = user.Email;
                payment.CartItemsId = (int)FarmId;
                payment.OrderTime = DateTime.Now;
                await paymentRepository.InsertAsync(payment, cancellationToken);
                HttpContext.Session.Remove("FarmId");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Farm");
       

    }

}
