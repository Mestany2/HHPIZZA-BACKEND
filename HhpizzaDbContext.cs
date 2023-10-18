using Microsoft.EntityFrameworkCore;
using HHPIZZA_BACKEND.Models;
using System.Runtime.CompilerServices;

namespace HHPIZZA_BACKEND
{
    public class HhpizzaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public HhpizzaDbContext(DbContextOptions<HhpizzaDbContext> context) : base(context)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed data with Users
            modelBuilder.Entity<User>().HasData(new User[]
            {
        new User {Id = 1, Name = "Jack Smith", Uid="4d56asd6", isStaff = true},
        new User {Id = 2, Name = "Mike James", Uid="S2KN1UC0fmc041tiwtKqHzA49zb2", isStaff = false},

            });

            modelBuilder.Entity<Order>().HasData(new Order[]
    {
         new Order {Id = 1, Name = "Customer One", Status = "Open", Email ="My@yahoo.com", Phone="615-123-4567", OrderType="Phone", PaymentId=1, UserId=1, ReviewId=1, Tip=5.45M},
         new Order {Id = 2, Name = "Customer Two", Status = "Closed", Email ="Myfg@yahoo.com", Phone="615-143-4667", OrderType="Online", PaymentId=2, UserId=2, ReviewId=2, Tip=2.45M},

    });
            modelBuilder.Entity<Item>().HasData(new Item[]
    {
        new Item { Id = 1, Name = "Cheese Pizza", Price = 4.99M},
        new Item { Id = 2, Name = "Wings", Price = 8.99M},

    });
            modelBuilder.Entity<Payment>().HasData(new Payment[]
    {
         new Payment {Id = 1, Type = "Credit Card"},
         new Payment {Id = 2, Type = "Cash"},

    });
            modelBuilder.Entity<Review>().HasData(new Review[]
    {
         new Review {Id = 1, Content = "Great", Rating=5},
         new Review {Id = 2, Content = "The best", Rating=4},



    });
            var ItemOrder = modelBuilder.Entity("ItemOrder");
            ItemOrder.HasData(new[]
                {
            new { ordersId = 1, itemsId = 1 },
            new { ordersId = 1, itemsId = 2 },
            });

        }
    }

}

