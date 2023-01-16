using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InternetShopDB
{
    public class InternetShContextFactory : IDesignTimeDbContextFactory<InternetShopContext>
    {
        public InternetShopContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InternetShopContext>();

            
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            
            string connectionString = config.GetConnectionString("MyConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new InternetShopContext(optionsBuilder.Options);
        }
    }
}
