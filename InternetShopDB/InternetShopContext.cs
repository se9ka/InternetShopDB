using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InternetShopDB
{
    public class InternetShopContext : DbContext
    {
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Provider> Providers { get; set; } = null!;
        public DbSet<Stock> Stocks { get; set; } = null!;
        public DbSet<Storage> Storages { get; set; } = null!;
        public DbSet<Delivery> Delivery { get; set; } = null!;
        public DbSet<ProductInOrder> ProductInOrders { get; set; } = null!;
        public IQueryable<Product> AmauntProd(int count) => FromExpression(() => AmauntProd(count));

        public InternetShopContext(DbContextOptions options) : base(options)
        {
         // Database.EnsureDeleted();
           Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string? connectionString = config.GetConnectionString("MyConnection");
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(connectionString);
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductInOrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.HasDbFunction(() => AmauntProd(default));
            /*  modelBuilder.Entity<Client>().HasData(
                  new Client { Id = 1, Name = "Cris", LastName = "Bums",PhoneNumb="0661947284",Email="C@gmail.com" },
                  new Client { Id = 2, Name = "Alice", LastName = "Clin", PhoneNumb = "0661947285", Email = "A@gmail.com" }*/
            // );
        }
        public class ProductInOrderConfiguration : IEntityTypeConfiguration<ProductInOrder>
        {
            public void Configure(EntityTypeBuilder<ProductInOrder> builder)
            {
                builder.HasOne(p => p.Product)
                 .WithMany(c => c.ProductsInOrders)
                 .HasForeignKey(u => u.ProductId);

                builder.HasOne(p => p.Order)
                .WithMany(c => c.ProductsInOrders)
                .HasForeignKey(u => u.OrderID);

                builder.HasKey(p => new { p.OrderID, p.ProductId });
            }
        }
        public class OrderConfiguration : IEntityTypeConfiguration<Order>
        {
            public void Configure(EntityTypeBuilder<Order> builder)
            {
                builder.HasOne(p => p.Client)
               .WithMany(c => c.Orders)
               .HasForeignKey(u => u.ClientId);

                builder.HasOne(p => p.Employee)
                .WithMany(c => c.Orders)
                .HasForeignKey(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

                builder.HasCheckConstraint("Sum", "Sum > 0");
               
            }
        }
        public class StockConfiguration : IEntityTypeConfiguration<Stock>
        {
            public void Configure(EntityTypeBuilder<Stock> builder)
            {
                builder.HasOne(p => p.Product)
              .WithMany(c => c.Stocks)
              .HasForeignKey(u => u.ProductId);

                builder.HasOne(p => p.Storage)
                .WithMany(c => c.Stocks)
                .HasForeignKey(u => u.StorageId);

                builder.HasKey(p => new { p.StorageId, p.ProductId });
                builder.HasCheckConstraint("Count", "Count > 0");
            }
        }
        
    }
}
