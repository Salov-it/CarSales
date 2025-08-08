using CarSales.Application.DTOs;
using CarSales.Application.Interfaces;
using CarSales.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Services
{
    public class SalesReportService : ISalesReportService
    {
        private readonly IOrderReadRepository _orderReadRepository;

        public SalesReportService(IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<List<MonthlySalesReportDto>> GetMonthlySalesReportAsync(int year)
        {
            var orders = await _orderReadRepository.GetOrdersByYearAsync(year);

            var monthNames = new[]
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };

            return orders
                .GroupBy(o => new { Brand = o.Brand.Name, Model = o.Model.Name })
                .Select(g =>
                {
                    var report = new MonthlySalesReportDto
                    {
                        Brand = g.Key.Brand,
                        Model = g.Key.Model,
                        Year = year
                    };

                    foreach (var order in g)
                    {
                        var monthName = monthNames[order.OrderDate.Month - 1];
                        report.MonthlySales[monthName] += order.Quantity * order.UnitPrice;
                    }

                    return report;
                })
                .ToList();
        }


    }
}
