using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_test2.Models;

public class Customer
{
    [Key] [Required] [DatabaseGenerated(DatabaseGeneratedOption.None)] public int CustomerId { get; set; }

    [Required] [MaxLength(50)] public string FirstName { get; set; } = null!;

    [Required] [MaxLength(100)] public string LastName { get; set; } = null!;

    [MaxLength(100)] [Phone] public string? PhoneNumber { get; set; }

    public ICollection<PurchasedTicket> PurchasedTickets { get; set; } = new List<PurchasedTicket>();
}