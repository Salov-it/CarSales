using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.CQRS.Queries.GetMonthlySalesExcel
{
    public record GetMonthlySalesExcelQuery(int Year, string? Model) : IRequest<byte[]>;

}
