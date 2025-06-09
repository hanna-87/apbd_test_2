using APBD_test2.Data;
using APBD_test2.DTOs;
using APBD_test2.Models;
using Microsoft.EntityFrameworkCore;
using TestFinal_APBD.Exceptions;

namespace APBD_test2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext dbContext)
    {
        _context = dbContext;
    }


    public async Task<int> CreateCustomerAsync(CreateCustomerDto request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == request.Customer.Id);
            if (customer != null)
            {
                throw new ConflictException("Customer already exists");
            }

            customer = new Customer()
            {
                CustomerId = request.Customer.Id,
                FirstName = request.Customer.FirstName,
                LastName = request.Customer.LastName,
                PhoneNumber = request.Customer.PhoneNumber
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            var ticketsGroupedByConcertName = request.Purchases
                .GroupBy(p => p.ConcertName);
            foreach (var concertGroup in ticketsGroupedByConcertName)
            {
                var concert = await _context.Concerts.FirstOrDefaultAsync(c => c.Name == concertGroup.Key);
                if (concert == null)
                {
                    throw new ConflictException("Concert not found");
                }

                if (concertGroup.Count() >= 5)
                {
                    throw new ConflictException("Not possible to purchase more then 5 tickets for one concert");
                }

                foreach (var purchaseDto in concertGroup)
                {
                    var ticket = new Ticket
                    {
                        SerialNumber = "Not-implemented-123",
                        SeatNumber = purchaseDto.SeatNumber
                    };
                    
                    await _context.Tickets.AddAsync(ticket);
                    await _context.SaveChangesAsync();

                    var tc = new TicketConcert
                    {
                        TicketId = ticket.TicketId,
                        ConcertId = concert.ConcertId,
                        Price = purchaseDto.Price
                    };
                    await _context.TicketConcerts.AddAsync(tc);
                    await _context.SaveChangesAsync();

                    var pt = new PurchasedTicket
                    {
                        TicketConcertId = tc.TicketConcertId,
                        CustomerId = customer.CustomerId,
                        PurchaseDate = DateTime.UtcNow
                    };
                    await _context.PurchasedTickets.AddAsync(pt);
                    await _context.SaveChangesAsync();
                }
            }
            await transaction.CommitAsync();
            return customer.CustomerId;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw e;
        }
    }


    public async Task<GetCustomerInfoDto> GetCustomerInfoAsync(int id)
    {
        var customer = await _context.Customers
            .Where(c => c.CustomerId == id)
            .Select(c => new GetCustomerInfoDto
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Purchases = c.PurchasedTickets
                    .Select(pt => new PurchaseInfoDto
                    {
                        Date = pt.PurchaseDate,
                        Price = pt.TicketConcert.Price,

                        Ticket = new TicketInfoDto
                        {
                            Serial = pt.TicketConcert.Ticket.SerialNumber,
                            SeatNumber = pt.TicketConcert.Ticket.SeatNumber
                        },

                        Concert = new ConcertInfoDto
                        {
                            Name = pt.TicketConcert.Concert.Name,
                            Date = pt.TicketConcert.Concert.Date
                        }
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (customer == null)
            throw new NotFoundException("Customer not found");

        return customer;
    }
    


}