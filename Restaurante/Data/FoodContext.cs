using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data
{
    public class FoodContext : DbContext
    {
        public FoodContext(DbContextOptions<FoodContext> opts)
            : base(opts)
        {

        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Addres> Addres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
