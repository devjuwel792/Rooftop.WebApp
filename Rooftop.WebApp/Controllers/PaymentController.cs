using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.RepositoryService;
using Rooftop.WebApp.ViewModel;
using System.Collections.Immutable;
using System.Threading;

namespace Rooftop.WebApp.Controllers;

public class PaymentController : Controller
{
    private readonly IPaymentRepository paymentRepository;

    public PaymentController(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }
    public async Task<IActionResult> Index()
    {

        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null)
       {
            ViewBag.path = HttpContext.Request.PathBase + HttpContext.Request.Path + HttpContext.Request.QueryString;
            var data = await paymentRepository.GetAllAsync(x=>x.User);
            //var result = data.Where(x=>x.IsPaymentConfirmed).ToList();
            //var result = from obj in data
            //             where (obj.IsPaymentConfirmed)
            //             select obj;
            var result = data.AsQueryable().Where(x=>x.IsPaymentConfirmed).ToList();
            return View(result);
       } 
        return RedirectToAction("Login", "Admin");

    }
    public async Task<ActionResult<HouseOwnerVm>> CreatePayment(int id, CancellationToken cancellationToken)
    {
            return View(new HouseOwnerVm());
    }
    public async Task<ActionResult<HouseOwnerVm>>  Pending()
    {
        ViewBag.path = HttpContext.Request.PathBase + HttpContext.Request.Path + HttpContext.Request.QueryString;
        var user = HttpContext.Session.GetString("adminEmail");
        if (user != null)
        {
            var data = await paymentRepository.GetAllAsync(x => x.User);
            var result = data.AsQueryable().Where(x => x.IsPaymentConfirmed).ToList();
            return View(result);
        }
        return RedirectToAction("Login", "Admin");
       
    }

    [HttpPost]
    public async Task<ActionResult<HouseOwnerVm>> Pending(int id,  CancellationToken cancellationToken)
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

}
