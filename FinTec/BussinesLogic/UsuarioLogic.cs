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
            return _repository.Add(oneElement);
        }
    }
}
