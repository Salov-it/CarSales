using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }

        // Навигация
        public Brand Brand { get;  set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
