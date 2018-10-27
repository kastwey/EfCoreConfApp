
namespace EfCoreTutoApp.Extensions
{
	using EfCoreTutoApp.Dtos;
	using EfCoreTutoApp.Entities;
	using System.Linq;

	public static class ReviewExtensions
	{
		public static IQueryable<ReviewDto> ToReviewDtoList(this IQueryable<Review> reviews)
		{
			var result = reviews.Select(x => new ReviewDto
			{
				NumStars = x.NumStars,
				VoterName = x.VoterName,
				ProductName = x.Product.Name
			});
			return result;
		}

		public static IQueryable<ReviewDto> ToReviewDtoListEager(this IQueryable<Review> reviews)
		{
			var result = reviews.Select(x => x.ToReviewDtoEager());
			return result;
		}

		public static ReviewDto ToReviewDtoEager(this Review review)
		{
			return new ReviewDto
			{
				NumStars = review.NumStars,
				ProductName = review.Product.Name,
				VoterName = review.VoterName
			};
		}
	}
}
