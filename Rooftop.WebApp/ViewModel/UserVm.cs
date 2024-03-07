using AutoMapper;
using Rooftop.WebApp.Models;

namespace Rooftop.WebApp.ViewModel;
[AutoMap(typeof(User),ReverseMap =true)]
public class UserVm
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}
