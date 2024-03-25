using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public interface IUserRepository:IRepositoryService<User,UserVm>
{
    public int CurrentUser(UserVm user);
}
