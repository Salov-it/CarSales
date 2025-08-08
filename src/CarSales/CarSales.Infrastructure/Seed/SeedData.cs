using CarSales.Domain.Entities;
using CarSales.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Seed
{
    public class SeedData
    {
        public static void Seed(CarSalesDbContext db)
        {
            if (db.Orders.Any())
                return; // уже засеяно

            var brands = new List<Brand>
            {
                new Brand { Name = "Audi" },
                new Brand { Name = "BMW" },
                new Brand { Name = "Mercedes-Benz" },
                new Brand { Name = "Toyota" },
                new Brand { Name = "Volkswagen" }
            };

            var models = new List<Model>
            {
                new Model { Name = "A4", Brand = brands[0] },
                new Model { Name = "A6", Brand = brands[0] },
                new Model { Name = "Q5", Brand = brands[0] },
                new Model { Name = "X3", Brand = brands[1] },
                new Model { Name = "X5", Brand = brands[1] },
                new Model { Name = "C-Class", Brand = brands[2] },
                new Model { Name = "E-Class", Brand = brands[2] },
                new Model { Name = "Camry", Brand = brands[3] },
                new Model { Name = "Corolla", Brand = brands[3] },
                new Model { Name = "Golf", Brand = brands[4] },
                new Model { Name = "Passat", Brand = brands[4] }
            };

            var colors = new List<Color>
            {
                new Color { Name = "Black" },
                new Color { Name = "White" },
                new Color { Name = "Gray" },
                new Color { Name = "Blue" },
                new Color { Name = "Red" }
            };

            var trims = new List<Trim>
            {
                new Trim { Name = "Base" },
                new Trim { Name = "Sport" },
                new Trim { Name = "Luxury" }
            };

            db.Brands.AddRange(brands);
            db.Models.AddRange(models);
            db.Colors.AddRange(colors);
            db.Trims.AddRange(trims);
            db.SaveChanges();

            var random = new Random();
            var startYear = DateTime.Now.Year - 4;
            var endYear = DateTime.Now.Year;

            var orders = new List<Order>();

            for (int year = startYear; year <= endYear; year++)
            {
                foreach (var model in models)
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        // генерим 1–5 заказов на модель в месяц
                        int ordersCount = random.Next(1, 6);

                        for (int i = 0; i < ordersCount; i++)
                        {
                            var orderDate = new DateTime(year, month, random.Next(1, 28));

                            orders.Add(new Order
                            {
                                OrderDate = orderDate,
                                BrandId = model.Brand.Id,
                                ModelId = model.Id,
                                ColorId = colors[random.Next(colors.Count)].Id,
                                TrimId = trims[random.Next(trims.Count)].Id,
                                Quantity = random.Next(1, 6), // 1–5 машин в заказе
                                UnitPrice = random.Next(2_000_000, 10_000_000), // 2–10 млн
                                Customer = $"Customer {random.Next(1, 1000)}"
                            });
                        }
                    }
                }
            }

            db.Orders.AddRange(orders);
            db.SaveChanges();
        }
    }
}
