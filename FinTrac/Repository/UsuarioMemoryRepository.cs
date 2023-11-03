using Domain;

namespace Repository
{
    public class UsuarioMemoryRepository : IRepository<Usuario>
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>();
        public Usuario Add(Usuario oneElement)
        {
            _usuarios.Add(oneElement);
            return oneElement;
        }

        public Usuario? Find(Func<Usuario, bool> filter)
        {
            return _usuarios.FirstOrDefault(filter);
        }

        public Usuario? Update(Usuario updateEntity)
        {
            var usuario = Find(u => u.Correo == updateEntity.Correo);
            if (usuario != null)
            {
                usuario.Contrasena = updateEntity.Contrasena;
                usuario.Correo = updateEntity.Correo;
            }
            return usuario;
        }

        public IList<Usuario> FindAll()
        {
            return _usuarios;
        }

    }
}
