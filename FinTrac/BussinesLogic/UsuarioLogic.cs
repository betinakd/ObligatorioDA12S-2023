using Excepcion;
using Domain;
using Repository;

namespace BussinesLogic
{
    public class UsuarioLogic
    {
        private readonly IRepository<Usuario> _repository;

        public UsuarioLogic(IRepository<Usuario> repository)
        {
            _repository = repository;
        }

        public Usuario AddUsuario(Usuario oneElement)
        {
            IList<Usuario> usuarios = _repository.FindAll();
            bool existe = usuarios.Contains(oneElement);
            if (existe)
            {
                throw new BussinesLogicUsuarioException("El usuario ya existe");
            }
            _repository.Add(oneElement);
            return oneElement;
        }

		public Usuario UsuarioByCorreoContrasena(string correo, string contrasena)
		{
			Usuario usuario = _repository.Find(u => u.Correo == correo);

			if (usuario != null && usuario.Contrasena == contrasena)
			{
				return usuario;
			}
			else
			{
				throw new BussinesLogicUsuarioException("El usuario o la contraseña no son válidos.");
			}
		}

        public IList<Usuario> FindAllUsuario()
        {
            return _repository.FindAll();
        }

        public void DeleteUsuario(string id)
        {
            _repository.Delete(id);
        }

        public Usuario? FindUsuario(string correo)
        {
            return _repository.Find(u => u.Correo == correo);
        }

        public void ModificarNombre(string correo, string nombre)
        {
			Usuario usuario = FindUsuario(correo);
			usuario.Nombre = nombre;
			_repository.Update(usuario);
		}

		public void ModificarApellido(string correo, string apellido)
		{
			Usuario usuario = FindUsuario(correo);
			usuario.Apellido = apellido;
			_repository.Update(usuario);
		}

        public void ModificarContrasena(string correo, string contrasena)
        {
			Usuario usuario = FindUsuario(correo);
			usuario.Contrasena = contrasena;
			_repository.Update(usuario);
		}

        public void ModificarDireccion(string correo, string direccion)
        {
            Usuario usuario = FindUsuario(correo);
            usuario.Direccion = direccion;
            _repository.Update(usuario);
        }

        public void CrearUsuario(string correo, string nombre, string apellido, string contrasena, string direccion)
        {
			Usuario usuario = new Usuario()
            {
				Correo = correo,
                Nombre = nombre,
                Apellido = apellido,
                Contrasena = contrasena,
                Direccion = direccion,
			};
            _repository.Add(usuario);
		}
	}
}
