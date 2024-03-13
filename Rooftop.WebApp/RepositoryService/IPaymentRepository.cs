using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public interface IPaymentRepository: IRepositoryService<Payment, PaymentVm>
{
}
