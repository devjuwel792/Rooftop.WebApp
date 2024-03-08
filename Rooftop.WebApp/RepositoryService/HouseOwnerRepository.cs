using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public class HouseOwnerRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryService<HouseOwners, HouseOwnerVm>(dbContext, mapper), IHouseOwnerRepository
{
    
}
