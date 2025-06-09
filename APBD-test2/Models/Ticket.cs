using System.ComponentModel.DataAnnotations;

namespace APBD_test2.Models;

public class Ticket
{
    [Key] [Required] public int TicketId { get; set; }

    [Required] [MaxLength(50)] public string SerialNumber { get; set; } = null!;

    [Required] public int SeatNumber { get; set; }

    public ICollection<TicketConcert> TicketConcerts { get; set; } = new List<TicketConcert>();
}