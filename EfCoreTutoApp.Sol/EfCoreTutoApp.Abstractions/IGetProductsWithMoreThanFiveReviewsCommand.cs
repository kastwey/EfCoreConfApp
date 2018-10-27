namespace EfCoreTutoApp.Abstractions
{
    public interface IGetProductsWithMoreThanFiveReviewsCommand : IDbCommand
	{
		bool UseDapper { get; set; }
	}
}
