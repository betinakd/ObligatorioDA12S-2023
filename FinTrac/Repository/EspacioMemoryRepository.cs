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
			.Include(e => e.Cambios)
			.Include(e => e.UsuariosInvitados)
			.Include(e => e.Cuentas)
			.Include(e => e.Categorias)
			.Include(e => e.Cambios)
			.Include(e => e.Objetivos)
			.ThenInclude(o => o.Categorias)
			.Include(e => e.Transacciones)
			.ThenInclude(t => t.CuentaMonetaria)
			.Include(e => e.Transacciones)
			.ThenInclude(t => t.CategoriaTransaccion)
			.ToList();
			return espacios;
		}

		public Espacio Update(Espacio updateEntity)
		{
			_context.Entry(updateEntity).State = EntityState.Modified;
			_context.SaveChanges();
			return updateEntity;
		}

		public void Delete(string id)
		{
			var espacio = _context.Espacios.Find(id);
			if (espacio != null)
			{
				_context.Espacios.Remove(espacio);
				_context.SaveChanges();
			}
		}
	}
}
