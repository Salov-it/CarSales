using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Persistence
{
    public class CarSalesDbContextFactory : IDesignTimeDbContextFactory<CarSalesDbContext>
    {
        public CarSalesDbContext CreateDbContext(string[] args)
        {
           
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../CarSales.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<CarSalesDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new CarSalesDbContext(optionsBuilder.Options);
        }
    
    }
}
