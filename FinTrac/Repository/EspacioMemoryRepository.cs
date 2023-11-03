using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class EspacioMemoryRepository : IRepository<Espacio>
	{
		private readonly FintracDbContext _context;
		public EspacioMemoryRepository(FintracDbContext context)
		{
			_context = context;
		}
		public Espacio Add(Espacio context)
		{
			_context.Espacios.Add(context);
			_context.SaveChanges();
			return context;
		}

		public Espacio? Find(Func<Espacio, bool> filter)
		{
			return _context.Espacios.FirstOrDefault(filter);
		}

		public IList<Espacio> FindAll()
		{
			var espacios = _context.Espacios
				.Include(e => e.Admin)
				.Include(e => e.Categorias)
				.ToList();
			return espacios;
		}

		public Espacio Update(Espacio updateEntity)
		{
			_context.Entry(updateEntity).State = EntityState.Modified;
			_context.SaveChanges();
			return updateEntity;
		}

	}
}
