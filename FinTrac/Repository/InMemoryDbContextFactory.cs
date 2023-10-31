using Microsoft.EntityFrameworkCore;

namespace Repository;

public interface IDbContextFactory
{
	UsuariosDbContext CreateDbContext();
}

public class InMemoryDbContextFactory : IDbContextFactory
{
	public UsuariosDbContext CreateDbContext()
	{
		var optionsBuilder = new DbContextOptionsBuilder<UsuariosDbContext>();
		optionsBuilder.UseInMemoryDatabase("UsuariosDbConectionTest");

		return new UsuariosDbContext(optionsBuilder.Options);
	}
}
