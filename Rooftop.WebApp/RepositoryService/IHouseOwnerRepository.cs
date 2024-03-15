using Microsoft.AspNetCore.Mvc.Rendering;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public interface IHouseOwnerRepository: IRepositoryService<HouseOwners, HouseOwnerVm>
{
    IEnumerable<SelectListItem> Dropdown();
    public int CurrentHouseOwner(HouseOwners houseOwners);
}
