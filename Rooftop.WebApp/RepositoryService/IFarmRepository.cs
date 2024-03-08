using Microsoft.AspNetCore.Mvc;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public interface IFarmRepository : IRepositoryService<farm, FarmVm>
{
    Task<ActionResult<FarmVm>> GetHouseOwnerFarm();
}
