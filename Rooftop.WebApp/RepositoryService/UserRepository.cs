using AutoMapper;
using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.Models;
using Rooftop.WebApp.Service;
using Rooftop.WebApp.ViewModel;

namespace Rooftop.WebApp.RepositoryService;

public class UserRepository(ApplicationDbContext dbContext, IMapper mapper) : RepositoryService<User, UserVm>(dbContext, mapper), IUserRepository
{
    
}
