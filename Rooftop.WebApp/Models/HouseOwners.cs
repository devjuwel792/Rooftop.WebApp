namespace Rooftop.WebApp.Models;

public class HouseOwners
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set;}
    public string Password { get; set;}
    public ICollection<farm> Farms { get; set; } =new HashSet<farm>();  
}
