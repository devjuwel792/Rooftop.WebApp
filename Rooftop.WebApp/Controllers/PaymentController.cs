using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.RepositoryService;
using Rooftop.WebApp.ViewModel;
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
            
            return View(data );
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
            return View(data);
        }
        return RedirectToAction("Login", "Admin");
       
    }

    [HttpPost]
    public async Task<ActionResult<HouseOwnerVm>> Pending(int id,  CancellationToken cancellationToken)
    {
       
        var payment = await paymentRepository.GetByIdAsync(id, cancellationToken);
        payment.IsPaymentConfirmed = true;
        
        await paymentRepository.UpdateAsync(id, payment, cancellationToken);
            return RedirectToAction("Index");
    }

}
