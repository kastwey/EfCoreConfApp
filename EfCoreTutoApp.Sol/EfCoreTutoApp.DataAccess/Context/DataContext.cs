
namespace EfCoreTutoApp.DataAccess.Context
{
    using EfCoreTutoApp.DataAccess.Configurations;
    using EfCoreTutoApp.Entities;
    using Microsoft.EntityFrameworkCore;
	using System;

	public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVendorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ProductVendor> ProductVendors { get; set; }

		[DbFunction("UDFAverageVotes", "DBO")]
		public double? UDFAverageVotes(Guid productId)
		{
			throw new NotImplementedException();
		}

	}
}