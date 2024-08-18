using OA.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace OA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; } 
        public DbSet<State> States { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(p => p.State)
                .WithMany()
                .HasForeignKey(p => p.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(p => p.City)
                .WithMany()
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuring the relationship between Category and Product
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // Configuring the relationship between ShoppingCartItem and User
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.User)
                .WithMany(u => u.ShoppingCartItems)
                .HasForeignKey(sci => sci.UserId);

            // Configuring the relationship between ShoppingCartItem and Product
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Product)
                .WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(sci => sci.ProductId);

            // Configuring the relationship between User and Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.State)
                .WithMany()
                .HasForeignKey(p => p.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.City)
                .WithMany()
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .Property(o => o.PaymentTypeId)
                .HasConversion<int>();

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderStatusId)
                .HasConversion<int>();
        }
    }
}
