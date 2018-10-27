
namespace EfCoreTutoApp.Dtos
{
    using System;

    public class ProductListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishedOn { get; set; }
        public string VendorsNames { get; set; }
        public int ReviewsCount { get; set; }
        public double? ReviewsAverage { get; set; }
    }
}
