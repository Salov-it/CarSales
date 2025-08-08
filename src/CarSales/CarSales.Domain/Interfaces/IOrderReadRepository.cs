using CarSales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.Interfaces
{
    public interface IOrderReadRepository
    {
        Task<List<Order>> GetOrdersByYearAsync(int year, string? model = null);

    }
}
