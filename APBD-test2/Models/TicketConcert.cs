using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_test2.Models;

[Table("Purchased_Ticket")]
public class TicketConcert
{
    [Key] [Required] public int TicketConcertId { get; set; }

    [Required]
    [ForeignKey(nameof(Ticket))]
    public int TicketId { get; set; }

    public Ticket Ticket { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Concert))]
    public int ConcertId { get; set; }

    public Concert Concert { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal")]
    [Precision(10, 2)]
    public decimal Price { get; set; }


    public ICollection<PurchasedTicket> PurchasedTickets { get; set; } = new List<PurchasedTicket>();
}