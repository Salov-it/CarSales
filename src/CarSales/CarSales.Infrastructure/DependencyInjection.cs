using CarSales.Application.Interfaces;
using CarSales.Domain.Interfaces;
using CarSales.Infrastructure.Excel;
using CarSales.Infrastructure.Persistence;
using CarSales.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarSalesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Репозитории
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();

            services.AddScoped<IExcelExportService, ExcelExportService>();


            return services;
        }
    }
}
