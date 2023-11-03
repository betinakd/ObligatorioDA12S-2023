using Repository;
using Domain;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace RepositoryTest
{
	[TestClass]
	public class EspacioRepositorySqlTest
	{
		private EspacioMemoryRepository _repository;
		private FintracDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
		public void SetUp()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new EspacioMemoryRepository(_context);
		}

		[TestCleanup]
		public void CleanUp()
		{
			_context.Database.EnsureDeleted();
		}
	}
}
