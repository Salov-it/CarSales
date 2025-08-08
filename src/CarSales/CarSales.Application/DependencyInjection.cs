using CarSales.Application.CQRS.Queries.GetMonthlySales;
using CarSales.Application.Interfaces;
using CarSales.Application.Services;
using CarSales.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
        
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Сервисы
            services.AddScoped<ISalesReportService, SalesReportService>();

            return services;
        }
    }
}
