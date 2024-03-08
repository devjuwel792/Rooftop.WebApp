using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public class FarmRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryService<farm, FarmVm>(dbContext, mapper), IFarmRepository
{
    public Task<ActionResult<FarmVm>> GetHouseOwnerFarm()
    {
        throw new NotImplementedException();
    }
}
