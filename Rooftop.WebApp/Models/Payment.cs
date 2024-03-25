namespace Rooftop.WebApp.Models;

public class Payment
{
    public int Id { get; set; }
    public string TrasnsId { get; set; }
    public string Email { get; set; }
    public bool IsPaymentConfirmed { get; set; }
    public int CartItemsId { get; set; }
    public DateTime? OrderTime { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public farm farm { get; set; }
}
