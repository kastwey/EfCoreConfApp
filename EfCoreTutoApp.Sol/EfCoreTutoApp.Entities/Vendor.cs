
namespace EfCoreTutoApp.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vendor
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public virtual ICollection<ProductVendor> ProductVendor { get; set; }
    }
}
