using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APBD_test2.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    ConcertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableTickets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.ConcertId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });

            migrationBuilder.CreateTable(
                name: "Purchased_Ticket",
                columns: table => new
                {
                    TicketConcertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    ConcertId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchased_Ticket", x => x.TicketConcertId);
                    table.ForeignKey(
                        name: "FK_Purchased_Ticket_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "ConcertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchased_Ticket_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket_Concert",
                columns: table => new
                {
                    TicketConcertId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket_Concert", x => new { x.TicketConcertId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_Ticket_Concert_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Concert_Purchased_Ticket_TicketConcertId",
                        column: x => x.TicketConcertId,
                        principalTable: "Purchased_Ticket",
                        principalColumn: "TicketConcertId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "ConcertId", "AvailableTickets", "Date", "Name" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Concert 1" },
                    { 2, 150, new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "B" },
                    { 3, 151, new DateTime(2001, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "C" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 2, "B", "Y", null },
                    { 3, "C", "Z", null },
                    { 4, "A", "X", "123-456-789" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "SeatNumber", "SerialNumber" },
                values: new object[,]
                {
                    { 1, 1, "1234rkk-123" },
                    { 2, 2, "hjgvjkn-123" },
                    { 3, 3, "jhvvopki-123" }
                });

            migrationBuilder.InsertData(
                table: "Purchased_Ticket",
                columns: new[] { "TicketConcertId", "ConcertId", "Price", "TicketId" },
                values: new object[,]
                {
                    { 1, 1, 10.01m, 1 },
                    { 2, 2, 12.00m, 2 },
                    { 3, 3, 15.09m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Ticket_Concert",
                columns: new[] { "CustomerId", "TicketConcertId", "PurchaseDate" },
                values: new object[,]
                {
                    { 4, 1, new DateTime(2000, 9, 8, 19, 9, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2009, 7, 6, 18, 37, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(2025, 2, 4, 18, 49, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchased_Ticket_ConcertId",
                table: "Purchased_Ticket",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchased_Ticket_TicketId",
                table: "Purchased_Ticket",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_Concert_CustomerId",
                table: "Ticket_Concert",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket_Concert");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Purchased_Ticket");

            migrationBuilder.DropTable(
                name: "Concerts");

            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
