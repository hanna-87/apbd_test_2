using APBD_test2.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_test2.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Concert> Concerts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<PurchasedTicket> PurchasedTickets { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketConcert> TicketConcerts { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Concert>().HasData(
            new Concert { ConcertId = 1, Name = "Concert 1", Date = new DateTime(2025, 1, 1), AvailableTickets = 100 },
            new Concert { ConcertId = 2, Name = "B", Date = new DateTime(2000, 2, 2), AvailableTickets = 150 },
            new Concert { ConcertId = 3, Name = "C", Date = new DateTime(2001, 3, 3), AvailableTickets = 151 }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 4, FirstName = "A", LastName = "X", PhoneNumber = "123-456-789" },
            new Customer { CustomerId = 2, FirstName = "B", LastName = "Y", PhoneNumber = null },
            new Customer { CustomerId = 3, FirstName = "C", LastName = "Z", PhoneNumber = null }
        );

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket { TicketId = 1, SerialNumber = "1234rkk-123", SeatNumber = 1 },
            new Ticket { TicketId = 2, SerialNumber = "hjgvjkn-123", SeatNumber = 2 },
            new Ticket { TicketId = 3, SerialNumber = "jhvvopki-123", SeatNumber = 3 }
        );

        modelBuilder.Entity<TicketConcert>().HasData(
            new TicketConcert { TicketConcertId = 1, TicketId = 1, ConcertId = 1, Price = 10.01m },
            new TicketConcert { TicketConcertId = 2, TicketId = 2, ConcertId = 2, Price = 12.00m },
            new TicketConcert { TicketConcertId = 3, TicketId = 3, ConcertId = 3, Price = 15.09m }
        );

        modelBuilder.Entity<PurchasedTicket>().HasData(
            new { TicketConcertId = 1, CustomerId = 4,
                PurchaseDate = new DateTime(2000, 9, 8, 19, 9, 0)
            },
            new
            { TicketConcertId = 2, CustomerId = 2,
                PurchaseDate = new DateTime(2009, 7, 6, 18, 37, 0)
            },
            new
            { TicketConcertId = 3, CustomerId = 3, PurchaseDate = new DateTime(2025, 2, 4, 18, 49, 0)
            }
        );
    }
}