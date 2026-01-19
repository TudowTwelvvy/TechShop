using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
    }
}
