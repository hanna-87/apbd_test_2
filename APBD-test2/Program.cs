
using APBD_test2.Data;
using APBD_test2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>( opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IDbService, DbService>();
var app = builder.Build();


app.MapControllers();


app.Run();