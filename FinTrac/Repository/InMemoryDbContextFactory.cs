using Microsoft.EntityFrameworkCore;

namespace Repository;


public interface IDbContextFactory
{
	FintracDbContext CreateDbContext();
}

public class InMemoryDbContextFactory : IDbContextFactory
{
	public FintracDbContext CreateDbContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<FintracDbContext>();
		optionsBuilder.UseInMemoryDatabase("FintracDbConectionTest");

		return new FintracDbContext(optionsBuilder.Options);
	}
}