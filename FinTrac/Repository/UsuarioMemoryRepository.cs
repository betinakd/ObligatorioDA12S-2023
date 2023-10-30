using Domain;

namespace Repository
{
    public class UsuarioMemoryRepository : IRepository<Usuario>
    {
		private readonly UsuariosDbContext _dbContext;

		public UsuarioMemoryRepository(UsuariosDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Usuario Add(Usuario oneElement)
		{
			_dbContext.Usuarios.Add(oneElement);
			_dbContext.SaveChanges();
			return oneElement;
		}

		public Usuario? Find(Func<Usuario, bool> filter)
        {
			return _dbContext.Usuarios.FirstOrDefault(filter);
		}

        public Usuario? Update(Usuario updateEntity)
		{
			var usuario = Find(u => u.Correo == updateEntity.Correo);
			if (usuario != null)
			{
				usuario.Contrasena = updateEntity.Contrasena;
				usuario.Correo = updateEntity.Correo;
				usuario.Nombre = updateEntity.Nombre;
				usuario.Apellido = updateEntity.Apellido;
				usuario.Direccion = updateEntity.Direccion;
				usuario.IdEspacioPrincipal = updateEntity.IdEspacioPrincipal;
				_dbContext.SaveChanges();
			}
			return usuario;
		}

        public void Delete(string id)
        {/*
            var usuario = Find(u => u.Correo == id);
            if (usuario != null)
            {
                _usuarios.Remove(usuario);
            } */
        }

        public IList<Usuario> FindAll()
        {
			return null;/*
            return _usuarios; */
		}

    }
}
