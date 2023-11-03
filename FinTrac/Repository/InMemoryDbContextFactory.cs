using Microsoft.EntityFrameworkCore;

namespace Repository;


public interface IAppContextFactory
{
	FintracDbContext CreateDbContext();
}

public class InMemoryDbContextFactory : IAppContextFactory
{
	public FintracDbContext CreateDbContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<FintracDbContext>();
		optionsBuilder.UseInMemoryDatabase("FintracDbConectionTest");

		return new FintracDbContext(optionsBuilder.Options);
	}
}