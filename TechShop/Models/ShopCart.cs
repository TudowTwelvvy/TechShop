using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Models
{
    public class ShopCart
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? Items { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class Item
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string? ProductId { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public bool IsBuy { get; set; }
    }
}
