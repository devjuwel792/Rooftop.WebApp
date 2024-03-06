namespace Rooftop.WebApp.Models;

public class Payment
{
    public int Id { get; set; }
    public string TrasnsId { get; set; }
    public string Email { get; set; }
    public bool IsPaymentConfirmed { get; set; }
    public farm CartItems { get; set; }
    public DateTime? OrderTime { get; set; }
    public int userId { get; set; }
    public User User { get; set; }
}
