using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository
{
    public class UsuarioMemoryRepository
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>();
        public Usuario Add(Usuario oneElement)
        {
            var usuario = Find(u => u.Correo == oneElement.Correo);
            if (usuario != null)
            {
                throw new Exception("El usuario ya existe");
            }
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

        public void Delete(string id)
        {
            var usuario = Find(u => u.Correo == id);
            if (usuario != null)
            {
                _usuarios.Remove(usuario);
            }
        }

        public IList<Usuario> FindAll()
        {
            return _usuarios;
        }

    }
}
