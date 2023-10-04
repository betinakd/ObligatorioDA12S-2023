﻿using System;
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
            if (!oneElement.Validar_Contrasena(oneElement.Contrasena))
            {
                throw new Exception("La contraseña no es valida");
            }
            if (!oneElement.Validar_Correo(oneElement.Correo))
            {
                throw new Exception("El correo no es valido");
            }
            return _repository.Add(oneElement);
        }


        public IList<Usuario> FindAllUsuario()
        {
            return _repository.FindAll();
        }
    }
}
