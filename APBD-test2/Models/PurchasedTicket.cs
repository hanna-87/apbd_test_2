using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_test2.Models;

[Table("Ticket_Concert")]
[PrimaryKey(nameof(TicketConcertId), nameof(CustomerId))]
public class PurchasedTicket
{
    [Required]
    [ForeignKey(nameof(TicketConcert))]
    public int TicketConcertId { get; set; }

    public TicketConcert TicketConcert { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;
    [Required] public DateTime PurchaseDate { get; set; }
}