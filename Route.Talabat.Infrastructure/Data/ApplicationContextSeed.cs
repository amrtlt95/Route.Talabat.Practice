using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Data
{
    public  static class ApplicationContextSeed
    {
        public async static Task DataSeed(ApplicationDbContext applicationDbContext)
        {
            #region Seeding Brands
            var brandsData = File.ReadAllText($"../Route.Talabat.Infrastructure/Data/DataSeeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

            if (!applicationDbContext.Brands.Any())
            {
                if (brands is not null && brands.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        applicationDbContext.Brands.Add(brand);
                    }
                }
                await applicationDbContext.SaveChangesAsync();

            }
            #endregion
            #region Seeding Categories
            var categoriesData = File.ReadAllText($"../Route.Talabat.Infrastructure/Data/DataSeeding/categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

            if (!applicationDbContext.Categories.Any())
            {
                if (categories is not null && categories.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        applicationDbContext.Categories.Add(category);
                    }
                }
                await applicationDbContext.SaveChangesAsync();

            }
            #endregion
            #region Seeding Products
            var productsData = File.ReadAllText($"../Route.Talabat.Infrastructure/Data/DataSeeding/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (!applicationDbContext.Products.Any())
            {
                if (products is not null && products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        applicationDbContext.Products.Add(product);
                    }
                }
                await applicationDbContext.SaveChangesAsync();

            }
            #endregion


            
        }
    }
}
