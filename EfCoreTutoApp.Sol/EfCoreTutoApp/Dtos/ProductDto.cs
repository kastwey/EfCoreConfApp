
namespace EfCoreTutoApp.Dtos
{
    using System;
    using System.Collections.Generic;

    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ReviewDto> ReviewDtos { get; set; }
    }
}
