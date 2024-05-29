using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;
using ModelsLibrary.DtoModels;
using Microsoft.EntityFrameworkCore.Migrations;
using ModelsLibrary.Enums;
namespace DataLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Store> stores { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<ShoppingCartItem> shoppingCartItems { get; set; }
        public DbSet<ShoppingCart> shoppingCart { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>()
           .HasMany(s => s.Sellers)
           .WithOne(u => u.WorkingInStore)
           .HasForeignKey(s => s.WorkingInStoreId);

            modelBuilder.Entity<Store>()
            .HasOne(s => s.User)
            .WithMany(u => u.OwnedStores)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Store).WithMany(p => p.Products)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Store");
            });

            modelBuilder
                .Entity<Product>()
                .ToTable(
                    "products",
                    b => b.IsTemporal(
            ));


            modelBuilder.Entity<Product>()
            .HasIndex(b => new { b.Barcode })
            .IsUnique();

            modelBuilder.Entity<Order>()
           .HasOne(o => o.Store)
           .WithMany(s => s.Orders)
           .HasForeignKey(o => o.StoreId)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ShoppingCart>()
            .HasMany(s => s.Items)
            .WithOne(i => i.ShoppingCart)
            .HasForeignKey(i => i.ShoppingCartId);

            SeedData(modelBuilder);

        }


        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasData(User.Create(1, "User1Name", "user1@gmail.com", "AQAAAAIAAYagAAAAELQKZC7R2WQxHd1uo0g3xQ2YiBvZtuglIUcqhsekqrBVG8u6+gUBNaXjTerGZoIUwQ==", UserTypes.Admin)
                );

                entity.HasData(User.Create(2, "Store owner", "storeowner@gmail.com", "AQAAAAIAAYagAAAAELQKZC7R2WQxHd1uo0g3xQ2YiBvZtuglIUcqhsekqrBVG8u6+gUBNaXjTerGZoIUwQ==", UserTypes.StoreOwner)
                );
            });


            // Generate 5 stores
            for (int i = 1; i <= 5; i++)
            {
                modelBuilder.Entity<Store>().HasData(
                    Store.Create(i, $"Store {i}", $"Address {i}", 2)
                );
            }

            var random = new Random();
            // Generate 5000 stores
            for (int i = 1; i <= 1000; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    int id = (i - 1) * 5 + j;
                    string barcode = GenerateUniqueBarcode(random, modelBuilder);

                    modelBuilder.Entity<Product>().HasData(
                        Product.Create(id, $"Product {id}", 100, 100, j, barcode)
                     );
                }
            }
        }


        private string GenerateUniqueBarcode(Random random, ModelBuilder modelBuilder)
        {
            string barcode;
            bool isUnique;

            do
            {
                barcode = GenerateRandomBarcode(random);
                isUnique = !modelBuilder.Model.FindEntityType(typeof(Product)).GetProperties()
                    .Any(p => p.Name == nameof(Product.Barcode) && (string)p.GetDefaultValue() == barcode);
            } while (!isUnique);

            return barcode;
        }

        private string GenerateRandomBarcode(Random random)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
