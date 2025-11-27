using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechShop.Models;

namespace TechShop.Services
{
    public class ProductsService : IProductsServices
    {
        private List<Product> products = new List<Product>();

        public async Task<List<Product>?> List()
        {
            await this.GetData();
            return products;
        }

        private async Task GetData()
        {
            products = new List<Product>();

            try {
                string fileName = "Products.json";

                //open the json file
                using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();

                //deserialize the json file into a list of products
                var options = new JsonSerializerOptions
                {
                    //ignore case sensitivity when matching property names
                    PropertyNameCaseInsensitive = true
                };

                products = JsonSerializer.Deserialize<List<Product>>(json, options) ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading products data: {ex.Message}");
            }
        }   
    }

    public interface IProductsServices
    {
        Task<List<Product>?> List();
    }
}
