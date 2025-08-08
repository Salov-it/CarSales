using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get;  set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int TrimId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Customer { get; set; }

   
        public Brand Brand { get; set; }
        public Model Model { get; set; }
        public Color Color { get; set; }
        public Trim Trim { get; set; }
    }
}
