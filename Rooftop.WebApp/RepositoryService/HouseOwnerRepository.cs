using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;
using System.Collections.Generic;

namespace Rooftop.WebApp.RepositoryService;

public class HouseOwnerRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryService<HouseOwners, HouseOwnerVm>(dbContext, mapper), IHouseOwnerRepository
{
    public IEnumerable<SelectListItem> Dropdown()
    {
        return DbSet.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
    }
}
