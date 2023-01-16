

using InternetShopDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

var optionsBuilder = new DbContextOptionsBuilder<InternetShopContext>();
var options = optionsBuilder.Options;

/*using (var context = new InternetShopContext(options))
{
    Provider provider1 = new Provider { Name = "Rozetka", Phone = "0661947284" };
    Provider provider2 = new Provider { Name = "Muztorg", Phone = "0661947666" };
    context.Providers.AddRange(provider1, provider2);
    Storage storage1 = new Storage { Address = "str. Shevchenko 1" };
    Storage storage2 = new Storage { Address = "str. Ozerna 2" };
    context.Storages.AddRange(storage1,storage2);
   
    Product product1 = new Product { Name = "IPhone 14", CurPrice = 40000, Category = "Phone",Producer="Apple"};
    Product product2 = new Product { Name = "IPhone 13", CurPrice = 30000, Category = "Phone", Producer = "Apple" };
    Product product3 = new Product { Name = "Galaxy S21", CurPrice = 25000, Category = "Phone", Producer = "Samsung" };
    Product product4 = new Product { Name = "Galaxy S21 Ultra", CurPrice = 35000, Category = "Phone", Producer = "Samsung" };
    Product product5 = new Product { Name = "Yamaha F310", CurPrice = 4000, Category = "Music instrument", Producer = "Yamaha" };
    Product product6 = new Product { Name = "Cort AD810", CurPrice = 4500, Category = "Music instrument", Producer = "Cort" };

    product1.Providers.Add(provider1);
    product2.Providers.Add(provider1);
    product3.Providers.Add(provider1);
    product4.Providers.Add(provider1);
    product5.Providers.Add(provider2);
    product6.Providers.Add(provider2);

    context.Products.AddRange(product1,product2,product3,product4,product5,product6);
    context.SaveChanges();
    Stock stock1 = new Stock {Count = 10,ProductId=1,StorageId=1};
    Stock stock2 = new Stock { Count = 5,ProductId = 2, StorageId = 1 };
    Stock stock3 = new Stock { Count = 2,ProductId = 3, StorageId = 1 };
    Stock stock4 = new Stock { Count = 1,ProductId = 4, StorageId = 1 };
    Stock stock5 = new Stock { Count = 7,ProductId = 5, StorageId = 2 };
    Stock stock6 = new Stock { Count = 6,ProductId = 6, StorageId = 2 };
    context.Stocks.AddRange(stock1, stock2, stock3, stock4, stock5, stock6);
    Client client1 = new Client {  Name = "Cris", LastName = "Bums", PhoneNumb = "0661947284", Email = "C@gmail.com" };
    Client client2 = new Client {  Name = "Alice", LastName = "Clin", PhoneNumb = "0661947285", Email = "A@gmail.com" };
    context.Clients.AddRange(client1, client2);
    Employee employee1 = new Employee { Name = "Arseniy", LastName = "Bogomolets", PhoneNumb = "0661947284", Email = "Aa@gmail.com",Salary=20000,TaxpayerId="123",Position="Maneger"};
    Employee employee2 = new Employee { Name = "Danilo", LastName = "Shevchenko", PhoneNumb = "0633891787", Email = "Dd@gmail.com",Salary = 10000, TaxpayerId = "666", Position = "Admin" };
    context.Employees.AddRange(employee1, employee2);
    context.SaveChanges();
    Order order1 = new Order {ClientId=1,EmployeeId=1,Employee = employee1, Sum=20,DateOfOrder=DateTime.Today,KindOfDelivery="Delivery",KindOfPayment="Card",StatusOrder="In proces"};
    Order order2 = new Order { ClientId = 1, EmployeeId = 1, Employee = employee1, Sum = 1, DateOfOrder = DateTime.Today, KindOfDelivery = "Delivery", KindOfPayment = "Card", StatusOrder = "In proces" };
    Order order3 = new Order { ClientId = 2, EmployeeId = 2, Employee = employee2, Sum = 1, DateOfOrder = DateTime.Today, KindOfDelivery = "Self pickup", KindOfPayment = "Cash", StatusOrder = "In proces" };
    context.Orders.AddRange(order1,order2,order3);
    context.SaveChanges();

    ProductInOrder productInOrder1 = new ProductInOrder { ProductId = 1, OrderID = 1, Count = 1, Price = 40000 };
    ProductInOrder productInOrder2 = new ProductInOrder { ProductId = 2, OrderID= 1, Count = 1, Price = 30000 };
    context.ProductInOrders.AddRange(productInOrder1,productInOrder2);
    context.SaveChanges();

}*/
using (var context = new InternetShopContext(options))
{
   /* var products = (from product in context.Products.Include(p => p.Stocks)
                    where product.Producer== "Apple"
                    select product).ToList();

    var products1 = context.Products.Include(p => p.Stocks).Where(p => p.Producer == "Apple");
    

    var IPhones = context.Products.Where(p => EF.Functions.Like(p.Name!, "%IPhone%"));

    Product? product1 = context.Products.Find(3);
    if (product1 != null)
    {
        context.Stocks.Where(p => p.ProductId == product1.Id).Load();
    }
    

    var ProductsSort = context.Products.OrderBy(p => p.CurPrice);
   

    var Products3 = from p in context.Products
                join s in context.Stocks on p.Id equals s.ProductId
                join o in context.ProductInOrders on p.Id equals o.ProductId
                join or in context.Orders on o.OrderID equals or.Id
                select new { Name = p.Name, Count = s.Count ,Sum=or.Sum};

    var users = context.Products.Join(context.Stocks,
        u => u.Id,
        c => c.ProductId,
        (u, c) => new
        {
            Name = u.Name,
            Id = c.StorageId,
            Count = c.Count,
        }).Join(context.Storages,
         a => a.Id,
         s => s.Id,   
         (a, s) => new
        {    
           Name = a.Name,
           Count = a.Count,
           Add = s.Address     
        });      

    var groups = context.Products.GroupBy(u => u.Producer).Select(g => new
    {
        g.Key,
        Count = g.Count(),
       
    });

    var Union=context.Clients.Select(p => new { Name = p.Name ,LName=p.LastName})
    .Union(context.Employees.Select(c => new { Name = c.Name, LName = c.LastName }));

    var Intersect = context.Products.Where(p => p.CurPrice > 5000)
        .Intersect(context.Products.Where(p => p.CurPrice < 35000));

    var Except = context.Products.Where(p => p.CurPrice > 5000)
        .Except(context.Products.Where(p => p.CurPrice < 35000));

    var Distinct = context.Products.Select(p => p.Producer).Distinct();

    bool result = context.Products.Any(u => u.Producer == "Apple");

    bool result1 = context.Products.All(u => u.Category == "Phone");

    int count = context.Products.Count(p=>p.Category.Contains("Music instrument"));

    decimal Max = context.Products.Max(p => p.CurPrice);
    decimal Min = context.Products.Min(p => p.CurPrice);
    decimal Avg = context.Products.Average(p => p.CurPrice);
    decimal Sum = context.Products.Sum(p => p.CurPrice);

    //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    var productNoTracking = context.Products.AsNoTracking().FirstOrDefault();
    if (productNoTracking != null)
    {
        productNoTracking.Name = "test";
        context.SaveChanges();
    }
    SqlParameter param = new SqlParameter("@count", 4);
    var count1 = context.Products.FromSqlRaw("SELECT * FROM dbo.AmauntProd(@count)", param).ToList();
    //var count2 =context.AmauntProd(4);
    SqlParameter param1 = new SqlParameter("@name", "IPhone 13");
    var Stocks= context.Stocks.FromSqlRaw("GetCountByProduct @name", param1).ToList();
*/
    //захист Lazy loading
    var productLazy = context.Products.ToList();
    AsyncTask asynctask = new AsyncTask(options);

    var task1 = Task.Run(() =>  asynctask.example());
    await task1;


}


