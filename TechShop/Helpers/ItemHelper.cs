using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Models;

namespace TechShop.Helpers
{
    public static class ItemHelper
    {
        public static Item CreateItemFromProduct(Product product)
        {
            return new Item
            {
                Id = Guid.NewGuid(),//Create a new unique identifier for the item
                ProductId = product.Id.ToString(),//Assign the product's ID to the item's ProductId
                Name = product.Name,//Assign the product's name to the item's Name
                Price = product.Price,//Assign the product's price to the item's Price
                Quantity = product.Stock, //Assign the product's stocT AS THE INITIAL Quantity
                Image = product.Image,//Assign the product's image URL to the item's Image
            };

        }
    }
}
