using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechShop.Models;
using TechShop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

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
            try
            {
                string fileName = "Products.json";

                // Open the JSON file
                using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();

                // Set options for the deserializer
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Ignore case in properties
                };

                // Deserialize the JSON
                products = JsonSerializer.Deserialize<List<Product>>(json, options) ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public interface IProductsServices
    {
        Task<List<Product>?> List();
    }
}
