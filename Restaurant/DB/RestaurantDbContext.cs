using Microsoft.EntityFrameworkCore;
using Restaurant.Decorator;
using Restaurant.Entities;

namespace Restaurant.DB;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
    {
    }

    public DbSet<VeganMenuItem> VeganMenuItems { get; set; }
    public DbSet<GlutenFreeMenuItem> GlutenFreeMenuItems { get; set; }
    public DbSet<SpicyMenuItem> SpicyMenuItems { get; set; }
    public DbSet<OrderTable> Orders { get; set; }
    public DbSet<BasicFeedback> Feedback { get; set; }
    public DbSet<PreorderedDish> PreorderedDishes { get; set; }
    public DbSet<EventOrder> EventOrders { get; set; }
    public DbSet<FeedbackWithPhotoDecorator> DecoratedFeedback { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FeedbackWithPhotoDecorator>()
            .HasKey(f => f.FeedbackId);

        modelBuilder.Entity<FeedbackWithPhotoDecorator>()
            .HasOne(f => f.Feedback)
            .WithOne()
            .HasForeignKey<FeedbackWithPhotoDecorator>(f => f.FeedbackId);
    }

    public void EnsureDatabaseCreated()
    {
        Database.EnsureCreated();
    }
}
