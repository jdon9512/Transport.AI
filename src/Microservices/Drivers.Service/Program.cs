using Drivers.Service.Application.Drivers.CreateDriver;
using Drivers.Service.Application.Drivers.GetDriver;
using Drivers.Service.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi API",
        Version = "v1",
        Description = "Web API en .NET 8 con Swagger"
    });
});

//builder.Services.AddDbContext<OrdersDbContext>(opt =>
//    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDbContext<DriversDbContext>(options =>
{
    options.UseInMemoryDatabase("TransportDB");
});

builder.Services.AddScoped<CreateDriverHandler>();
builder.Services.AddScoped<GetDriverHandler>();

var app = builder.Build();

// Seed de datos (opcional pero recomendado)
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();

//    if (!db.Productos.Any())
//    {
//        db.Productos.AddRange(
//            new Producto { Id = 1, Nombre = "Teclado", Precio = 100 },
//            new Producto { Id = 2, Nombre = "Mouse", Precio = 50 }
//        );

//        db.SaveChanges();
//    }
//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();