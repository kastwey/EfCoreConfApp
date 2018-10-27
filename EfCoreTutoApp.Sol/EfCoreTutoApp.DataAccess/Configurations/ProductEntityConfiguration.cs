
namespace EfCoreTutoApp.DataAccess.Configurations
{
    using EfCoreTutoApp.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.PublishedOn).HasColumnType("date");
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)");
            builder.Property(x => x.ImageUrl).IsUnicode(false);
            builder.HasIndex(x => x.PublishedOn);
            builder.HasQueryFilter(p => !p.SoftDeleted);
            builder.Property(x => x.RedeableDate)
                .HasComputedColumnSql("LEFT(DATENAME(MONTH,[PublishedOn]),3) + ' ' + RIGHT('00' + CAST(YEAR([PublishedOn]) AS VARCHAR),2)");
        }
    }
}
