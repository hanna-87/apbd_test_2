using System.ComponentModel.DataAnnotations;

namespace APBD_test2.Models;

public class Concert
{
    [Key] [Required] public int ConcertId { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; } = null!;

    [Required] public DateTime Date { get; set; }

    [Required] public int AvailableTickets { get; set; }

    public ICollection<TicketConcert> TicketConcerts { get; set; } = new List<TicketConcert>();
}