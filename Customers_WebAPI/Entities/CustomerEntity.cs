using System.ComponentModel.DataAnnotations;

namespace Customers_WebAPI.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Biography { get; set; }
    public string? ProfileImage { get; set; } = "avatar.jpg";

    public string StreetName { get; set; } = null!;
    public string? StreetName_2 { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public string? UserId { get; set; }

}
