using System.ComponentModel.DataAnnotations;

namespace APBD_test2.DTOs;

public class CreateCustomerDto
{
    [Required]
    public CustomerDto Customer { get; set; } = null!;

    [Required]
    public List<PurchaseDto> Purchases { get; set; } = new List<PurchaseDto>();
}

public class CustomerDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [MaxLength(100)]
    [Phone]
    public string? PhoneNumber { get; set; }
}

public class PurchaseDto
{
    [Required]
    public int SeatNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public string ConcertName { get; set; } = null!;

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
}