using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;
using System.Linq.Expressions;

namespace Rooftop.WebApp.RepositoryService;

public class PaymentRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryService<Payment, PaymentVm>(dbContext, mapper), IPaymentRepository
{
 

  
}
