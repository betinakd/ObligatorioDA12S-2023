using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EspacioMemoryRepository : IRepository<Espacio>
    {
        private readonly FintracDbContext _espacios;
        public EspacioMemoryRepository(FintracDbContext espacios)
        {
			_espacios = espacios;
		}
		public Espacio Add(Espacio oneElement)
		{
			_espacios.Espacios.Add(oneElement);
			_espacios.SaveChanges();
			return oneElement;
		}

		public Espacio Find(Func<Espacio, bool> filter)
		{
			return _espacios.Espacios.FirstOrDefault(filter);
		}

		public IList<Espacio> FindAll()
		{
			var espacios = _espacios.Espacios
				.Include(e => e.Admin)
				.Include(e => e.Cambios)
				.Include(e => e.UsuariosInvitados)
				.Include(e => e.Cuentas)
				.Include(e => e.Categorias)
				.Include(e => e.Cambios)
				.Include(e => e.Objetivos)
					.ThenInclude(o => o.Categorias)
				.Include(e => e.Transacciones)
				.ToList();

			return espacios;
		}

		public Espacio Update(Espacio updateEntity)
		{
			_espacios.Entry(updateEntity).State = EntityState.Modified;
			_espacios.SaveChanges();
			return updateEntity;
		}

		public void Delete(string id)
		{
			var espacio = _espacios.Espacios.Find(id);
			if (espacio != null)
			{
				_espacios.Espacios.Remove(espacio);
				_espacios.SaveChanges();
			}
		}
	}
}