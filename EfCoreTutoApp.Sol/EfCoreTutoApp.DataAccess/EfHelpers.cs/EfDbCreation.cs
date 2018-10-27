
namespace EfCoreTutoApp.DataAccess.EfHelperscs
{
    using EfCoreTutoApp.DataAccess.Context;
    using EfCoreTutoApp.Entities;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Storage;
    using System;
    using System.Linq;

    public static class EfDbCreation
    {
        private const string SeedProductsSearchName = "products.json";
        private const string SeedVendorsSearchName = "vendors.json";
        private const string SeedProductsVendorSearchName = "productVendor.json";
        private const string SeedReviewsSearchName = "reviews.json";

        public static void CreateDataBase(string connectionString)
        {
            using (var db = new DataContext(EfDbContext.SetupOptionsWithConnectionString(null, connectionString)))
            {
                Console.WriteLine("Checking if database exists...");
                if ((db.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                {
                    Console.WriteLine("Database already created.");
                }
                else
                {
                    Console.WriteLine("Database doesn't exist. Creating...");
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    Console.WriteLine("Database created.");
                }
                SeedDatabase(db);
            }
        }

        private static void SeedDatabase(DataContext db)
        {
            if (!db.Products.Any())
            {
                Console.WriteLine("Seeding database...");

                var products = JsonLoader.LoadJson<Product>(SeedProductsSearchName);
                var vendors = JsonLoader.LoadJson<Vendor>(SeedVendorsSearchName);
                var productVendors = JsonLoader.LoadJson<ProductVendor>(SeedProductsVendorSearchName);
                var reviews = JsonLoader.LoadJson<Review>(SeedReviewsSearchName);

                db.AddRange(products);
                db.AddRange(vendors);
                db.AddRange(productVendors);
                db.AddRange(reviews);

                db.SaveChanges();

                Console.WriteLine("Database seeded.");
            }
        }
    }
}
