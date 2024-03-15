using AutoMapper;
using Rooftop.WebApp.Models;

namespace Rooftop.WebApp.ViewModel;
[AutoMap(typeof(Payment), ReverseMap = true)]
public class PaymentVm
{
    public int Id { get; set; }
    public string TrasnsId { get; set; }
    public string Email { get; set; }
    public bool IsPaymentConfirmed { get; set; }
    public farm CartItems { get; set; }
    public DateTime? OrderTime { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
