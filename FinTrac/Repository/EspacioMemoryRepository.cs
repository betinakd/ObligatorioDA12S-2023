using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain;

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

		public Espacio Find(Func<Espacio, bool> filter)
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
				.Include(e => e.Transacciones)
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
