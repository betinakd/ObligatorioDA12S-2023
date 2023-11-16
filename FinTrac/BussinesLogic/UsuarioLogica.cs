using Excepcion;
using Dominio;
using Repositorio;

namespace LogicaNegocio
{
	public class UsuarioLogica
	{
		private readonly IRepositorio<Usuario> _repository;

		public UsuarioLogica(IRepositorio<Usuario> repository)
		{
			_repository = repository;
		}

		public Usuario AgregarUsuario(Usuario oneElement)
		{
			IList<Usuario> usuarios = _repository.FindAll();
			bool existe = usuarios.Contains(oneElement);
			if (existe)
			{
				throw new LogicaNegocioUsuarioExcepcion("El usuario ya existe");
			}
			_repository.Add(oneElement);
			return oneElement;
		}

		public Usuario UsuarioPorCorreoContrasena(string correo, string contrasena)
		{
			Usuario usuario = _repository.Find(u => u.Correo == correo);

			if (usuario != null && usuario.Contrasena.Equals(contrasena))
			{
				return usuario;
			}
			else
			{
				throw new LogicaNegocioUsuarioExcepcion("El usuario o la contraseña no son válidos.");
			}
		}

		public IList<Usuario> DarUsuarios()
		{
			return _repository.FindAll();
		}

		public void BorrarUsuario(string id)
		{
			_repository.Delete(id);
		}

		public Usuario? EncontrarUsuario(string correo)
		{
			return _repository.Find(u => u.Correo == correo);
		}

		public void ModificarNombre(string correo, string nombre)
		{
			Usuario usuario = EncontrarUsuario(correo);
			usuario.Nombre = nombre;
			_repository.Update(usuario);
		}

		public void ModificarApellido(string correo, string apellido)
		{
			Usuario usuario = EncontrarUsuario(correo);
			usuario.Apellido = apellido;
			_repository.Update(usuario);
		}

		public void ModificarContrasena(string correo, string contrasena)
		{
			Usuario usuario = EncontrarUsuario(correo);
			usuario.Contrasena = contrasena;
			_repository.Update(usuario);
		}

		public void ModificarDireccion(string correo, string direccion)
		{
			Usuario usuario = EncontrarUsuario(correo);
			usuario.Direccion = direccion;
			_repository.Update(usuario);
		}

		public void CrearUsuario(Usuario usuario)
		{
			AgregarUsuario(usuario);
		}

		public List<Usuario> UsuariosNoPresentesEspacio(Espacio espacio)
		{
			IList<Usuario> usuarios = DarUsuarios();
			List<Usuario> usuariosNoPresentes = new List<Usuario>();

			foreach (var usuario in usuarios)
			{
				if (!espacio.PerteneceCorreo(usuario.Correo))
				{
					usuariosNoPresentes.Add(usuario);
				}
			}

			return usuariosNoPresentes;
		}
	}
}
