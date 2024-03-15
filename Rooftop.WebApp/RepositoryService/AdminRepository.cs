using AutoMapper;
using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public class AdminRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryService<Admin, AdminVm>(dbContext, mapper), IAdminRepository
{
   
    public bool Current(Admin user)
    {
        
        foreach (var item in DbSet)
        {
            if (item.Email == user.Email && item.Password == user.Password)
            {

               var  admin = true;
                return admin;
            }
        }
        return false;
    }
}