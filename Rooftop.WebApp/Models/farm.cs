namespace Rooftop.WebApp.Models;

public class farm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public long price { get; set; }
    public double SquareFeet { get; set; }
    public string Location { get; set; }
    public string Image1 { get; set; }
    public string Image2 { get; set; }
    public string Image3 { get; set; }
    public string GoogleMap { get; set; }
    public string SomeDetail { get; set; }
    public string MoreDetail { get; set; }
    public int HouseOwnersId { get; set; }
    public HouseOwners HouseOwners { get; set; }    
}
