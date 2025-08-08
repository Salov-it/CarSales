using CarSales.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.CQRS.Queries.GetMonthlySales
{
    public record GetMonthlySalesQuery(int Year, string Model = null): IRequest<List<MonthlySalesReportDto>>;


}
