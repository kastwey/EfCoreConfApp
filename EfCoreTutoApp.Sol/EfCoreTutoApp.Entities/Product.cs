
namespace EfCoreTutoApp.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Guid Id { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public string RedeableDate { get; private set; }
        public decimal Price { get; set; }
        [MaxLength(512)]
        public string ImageUrl { get; set; }
        public bool SoftDeleted { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ProductVendor> ProductVendor { get; set; }
    }
}
