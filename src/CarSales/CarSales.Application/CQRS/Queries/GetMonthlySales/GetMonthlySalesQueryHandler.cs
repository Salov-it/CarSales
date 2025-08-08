using CarSales.Application.DTOs;
using CarSales.Application.Interfaces;
using CarSales.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.CQRS.Queries.GetMonthlySales
{
    public class GetMonthlySalesQueryHandler : IRequestHandler<GetMonthlySalesQuery, List<MonthlySalesReportDto>>
    {
        private readonly IOrderReadRepository _orderReadRepository;

        public GetMonthlySalesQueryHandler(IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<List<MonthlySalesReportDto>> Handle(GetMonthlySalesQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderReadRepository.GetOrdersByYearAsync(request.Year);

            // Фильтр по модели
            if (!string.IsNullOrWhiteSpace(request.Model))
                orders = orders
                    .Where(o => o.Model.Name.Equals(request.Model, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            var monthNames = new[]
            {
                "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
                "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
            };

            return orders
                .GroupBy(o => new { Brand = o.Brand.Name, Model = o.Model.Name })
                .Select(g =>
                {
                    var report = new MonthlySalesReportDto
                    {
                        Brand = g.Key.Brand,
                        Model = g.Key.Model,
                        Year = request.Year
                    };

                    
                    foreach (var month in monthNames)
                        report.MonthlySales[month] = 0m;

                    
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
  

