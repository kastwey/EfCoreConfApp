
namespace EfCoreTutoApp.Entities
{
    using System;

    public class ProductVendor
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public int Order { get; set; }
    }
}
