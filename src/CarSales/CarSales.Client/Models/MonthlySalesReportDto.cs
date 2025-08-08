using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Client.Models
{
   public class MonthlySalesReportDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Dictionary<string, decimal> MonthlySales { get; set; }
        public decimal Total { get; set; }
    }
}
