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

            // Если в запросе указана модель — фильтруем
            if (!string.IsNullOrWhiteSpace(request.Model))
                orders = orders.Where(o => o.Model.Name.Equals(request.Model, StringComparison.OrdinalIgnoreCase)).ToList();

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

                    foreach (var order in g)
                    {
                        var monthName = order.OrderDate.ToString("MMMM", CultureInfo.InvariantCulture);
                        report.MonthlySales[monthName] += order.Quantity * order.UnitPrice;
                    }

                    return report;
                })
                .ToList();
        }
    }
  
}
