namespace Rooftop.WebApp.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

}
