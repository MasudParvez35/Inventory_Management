using Microsoft.EntityFrameworkCore;
using OA.Core.Domain;

namespace OA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingCartItem> shoppingCartItems {  get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
            /*modelBuilder.Entity<Order>()
                .HasOne(sci => sci.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(sci => sci.UserId);*/
        }
    }
}
