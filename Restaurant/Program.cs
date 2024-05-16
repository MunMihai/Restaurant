using Microsoft.EntityFrameworkCore;
using Restaurant.Builder;
using Restaurant.Command;
using Restaurant.DB;
using Restaurant.FactoryMethod;
using Restaurant.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPreorderedDishRepository, PreorderedDishRepository>();
builder.Services.AddScoped<MenuItemFactory>();
builder.Services.AddScoped<EventBuilder>();
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(GetAllOrdersQuery).Assembly); });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
