using Microsoft.EntityFrameworkCore;

namespace Repositorio;

public interface IDbContextFactory
{
	FintracDbContext CreateDbContext();
}

public class InMemoryDbContextFactory : IDbContextFactory
{
	public FintracDbContext CreateDbContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<FintracDbContext>();
		optionsBuilder.UseInMemoryDatabase("UsuariosDbConectionTest");

		return new FintracDbContext(optionsBuilder.Options);
	}
}
