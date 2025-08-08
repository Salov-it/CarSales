using CarSales.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Interfaces
{
    public interface ISalesReportService
    {
        Task<List<MonthlySalesReportDto>> GetMonthlySalesReportAsync(int year);
    }
}
