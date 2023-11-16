using Repositorio;

namespace RepositorioTest
{
	[TestClass]
	public class InMemoryDbContextFactoryTest
	{
		[TestMethod]
		public void CreateDbContext()
		{
			var factory = new InMemoryDbContextFactory();
			var context = factory.CreateDbContext();
			Assert.IsNotNull(context);
		}
	}
}
