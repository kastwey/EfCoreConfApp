
namespace EfCoreTutoApp.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string VoterName { get; set; }
        public int NumStars { get; set; }
        public string Comment { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
