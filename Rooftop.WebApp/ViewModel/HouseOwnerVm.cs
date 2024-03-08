using AutoMapper;
using Rooftop.WebApp.Models;

namespace Rooftop.WebApp.ViewModel;
[AutoMap(typeof(HouseOwners), ReverseMap = true)]
public class HouseOwnerVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
