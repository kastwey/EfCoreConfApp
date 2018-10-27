
namespace EfCoreTutoApp.DataAccess.Configurations
{
    using EfCoreTutoApp.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductVendorEntityConfiguration : IEntityTypeConfiguration<ProductVendor>
    {
        public void Configure(EntityTypeBuilder<ProductVendor> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.VendorId });

            builder.HasOne(b => b.Product)
                .WithMany(pv => pv.ProductVendor)
                .HasForeignKey(b => b.ProductId);

            builder.HasOne(v => v.Vendor)
                .WithMany(pv => pv.ProductVendor)
                .HasForeignKey(v => v.VendorId);
        }
    }
}