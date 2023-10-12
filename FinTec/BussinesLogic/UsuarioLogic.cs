using Models.Domain;
using Repository;
using BussinesLogic.Excepcion;

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

        public Usuario? FindUsuario(string id)
        {
            return _repository.Find(u => u.Correo == id);
        }
    }
}
