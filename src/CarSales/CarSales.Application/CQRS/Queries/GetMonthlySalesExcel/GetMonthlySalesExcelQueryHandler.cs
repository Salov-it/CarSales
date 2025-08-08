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

namespace CarSales.Application.CQRS.Queries.GetMonthlySalesExcel
{
    public class GetMonthlySalesExcelQueryHandler : IRequestHandler<GetMonthlySalesExcelQuery, byte[]>
    {
        private readonly IOrderReadRepository _orderRepository;
        private readonly IExcelExportService _excelExportService;

        public GetMonthlySalesExcelQueryHandler(IOrderReadRepository orderRepository, IExcelExportService excelExportService)
        {
            _orderRepository = orderRepository;
            _excelExportService = excelExportService;
        }

        public async Task<byte[]> Handle(GetMonthlySalesExcelQuery request, CancellationToken ct)
        {
            var orders = await _orderRepository.GetOrdersByYearAsync(request.Year);

            if (!string.IsNullOrWhiteSpace(request.Model))
            {
                orders = orders
                    .Where(o => o.Model.Name.Equals(request.Model, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var reports = orders
                .GroupBy(o => new { BrandName = o.Brand.Name, ModelName = o.Model.Name })
                .Select(g =>
                {
                    var report = new MonthlySalesReportDto
                    {
                        Brand = g.Key.BrandName,
                        Model = g.Key.ModelName,
                        Year = request.Year
                    };

                    foreach (var order in g)
                    {
                        string monthName = order.OrderDate.ToString("MMMM", new CultureInfo("ru-RU"));
                        report.MonthlySales[monthName] += order.Quantity * order.UnitPrice;
                    }

                    return report;
                })
                .ToList();


            return _excelExportService.ExportMonthlySalesToExcel(reports);
        }
    }
}
