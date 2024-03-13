using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public interface IAdminRepository : IRepositoryService<Admin, AdminVm>
{
    bool Current(Admin user);
}
