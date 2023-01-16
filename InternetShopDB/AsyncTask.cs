using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InternetShopDB;

namespace InternetShopDB
{
    public class AsyncTask
    {
        private DbContextOptions options;
        public AsyncTask(DbContextOptions options)
        {
            this.options = options;
        }
        public async Task AsyncAdd()
        {
            using (InternetShopContext context = new InternetShopContext(options))
            {
                for (int i = 0; i < 10; i++)
                {
                    await context.Persons.AddAsync(new Person { Name = "Person " + i, LastName = " " });
                }
               await context.SaveChangesAsync();
            }
        }

        
        public ValueTask<Product?> FindProd()
        {
            using (InternetShopContext context = new InternetShopContext(options))
            {
                return context.Products.FindAsync(1);
            }

        }
        public void example()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Example");
        }
        //await FindProd

        public async Task AsyncRead()
        {
            using (InternetShopContext context = new InternetShopContext(options))
            {
                var persons = await context.Persons.ToListAsync();
                foreach (var item in persons)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

       
    }
}
