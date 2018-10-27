namespace EfCoreTutoApp.Abstractions
{
    public interface IUserDefinedFunctionCommand : IDbCommand
	{
		bool UseUDF { get; set; }
	}
}
