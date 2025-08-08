using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.DTOs
{
    public class MonthlySalesReportDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Dictionary<string, decimal> MonthlySales { get; set; }
           = new Dictionary<string, decimal>
           {
                ["январь"] = 0,
                ["февраль"] = 0,
                ["март"] = 0,
                ["апрель"] = 0,
                ["май"] = 0,
                ["июнь"] = 0,
                ["июль"] = 0,
                ["август"] = 0,
                ["сентябрь"] = 0,
                ["октябрь"] = 0,
                ["ноябрь"] = 0,
                ["декабрь"] = 0
           };


        public decimal Total => MonthlySales.Values.Sum();
    }
}
