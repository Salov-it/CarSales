using CarSales.Application.DTOs;
using CarSales.Domain.Entities;
using CarSales.Domain.Interfaces;
using CarSales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Repositories
{
    internal class OrderReadRepository : IOrderReadRepository
    {
        private readonly CarSalesDbContext _context;

        public OrderReadRepository(CarSalesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByYearAsync(int year, string? model = null)
        {
            var start = new DateTime(year, 1, 1);
            var end = new DateTime(year, 12, 31);

            var query = _context.Orders
                .Include(o => o.Brand)
                .Include(o => o.Model)
                .Where(o => o.OrderDate >= start && o.OrderDate <= end);

            if (!string.IsNullOrWhiteSpace(model))
                query = query.Where(o => o.Model.Name == model);

            return await query.ToListAsync();
        }

    }
}
