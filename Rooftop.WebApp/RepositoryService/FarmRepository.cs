using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public class FarmRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryService<farm, FarmVm>(dbContext, mapper), IFarmRepository
{
    public  List<farm> GetHouseOwnerFarmAsync(int id)
    {
        var data =  DbSet.Where(x => x.HouseOwnersId == id).ToList();
        
        return data;
    }
}
