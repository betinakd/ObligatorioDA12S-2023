using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EspacioMemoryRepository : IRepository<Espacio>
    {
		private readonly UsuariosDbContext _context;

		public EspacioMemoryRepository(UsuariosDbContext context)
		{
			_context = context;
		}

		public Espacio Add(Espacio oneElement)
        {
			_context.Espacios.Add(oneElement);
			_context.SaveChanges();
			return oneElement;
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
				.ToList();

			return espacios;
		}

		public Espacio? Update(Espacio updateEntity)
		 {
			return null;
			/* var espacio = Find(u => u.Admin == updateEntity.Admin);
			 if (espacio != null)
			 {               
				 espacio.Admin = updateEntity.Admin;
			 }
			 return espacio;*/
		}

		public void Delete(string id)
        {
			/*  var espacio = Find(u => u.Admin.Correo == id);
			  if (espacio != null)
			  {
				  _espacios.Remove(espacio);
			  } */
		}
    }
}
