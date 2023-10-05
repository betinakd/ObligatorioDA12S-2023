using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public bool ExisteCorreoUsuario(string correo)
		{
			IList<Usuario> usuarios = _repository.FindAll();
			bool existe = usuarios?.Any(u => u.Correo == correo) ?? false;
			return existe;
		}

		public Usuario UsuarioByCorreoContrasena(string correo, string contrasena)
		{
            if (ExisteCorreoUsuario(correo))
            {
                Usuario usuario = _repository.Find(u => u.Correo == correo);

                if (usuario.Contrasena == contrasena)
                {
                    return usuario;
                }
                else { 
                    throw new BussinesLogicUsuarioException("La contraseña no es valida, porfavor ingresela nuevamente.");
                }
            }
            else
            {
                throw new BussinesLogicUsuarioException("El usuario no existe");
            }
            return null;
		}

		public Usuario? UpdateUsuario(Usuario updateEntity)
        {
            ValidarCorreoYContrasena(updateEntity);
            return _repository.Update(updateEntity);
        }


        private void ValidarCorreoYContrasena(Usuario oneElement)
        {
            if (!oneElement.Validar_Contrasena(oneElement.Contrasena))
            {
                throw new Exception("La contraseña no es valida");
            }
            if (!oneElement.Validar_Correo(oneElement.Correo))
            {
                throw new Exception("El correo no es valido");
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
