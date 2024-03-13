using AutoMapper;
using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public class PaymentRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryService<Payment, PaymentVm>(dbContext, mapper), IPaymentRepository
{

}
