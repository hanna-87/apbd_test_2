using System.ComponentModel.DataAnnotations;

namespace APBD_test2.DTOs;

public class GetCustomerInfoDto
{
    [Required] [MaxLength(50)] public string FirstName { get; set; } = null!;

    [Required] [MaxLength(100)] public string LastName { get; set; } = null!;

    [MaxLength(100)] [Phone] public string? PhoneNumber { get; set; }

    [Required] public List<PurchaseInfoDto> Purchases { get; set; } = new List<PurchaseInfoDto>();
}

public class PurchaseInfoDto
{
    [Required] public DateTime Date { get; set; }

    [Required] [Range(0, double.MaxValue)] public decimal Price { get; set; }

    [Required] public TicketInfoDto Ticket { get; set; } = null!;

    [Required] public ConcertInfoDto Concert { get; set; } = null!;
}

public class TicketInfoDto
{
    [Required] [MaxLength(50)] public string Serial { get; set; } = null!;

    [Required] public int SeatNumber { get; set; }
}

public class ConcertInfoDto
{
    [Required] [MaxLength(100)] public string Name { get; set; } = null!;

    [Required] public DateTime Date { get; set; }
}