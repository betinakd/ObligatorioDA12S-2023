using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic
{
    public class EspacioLogic
    {
        private readonly IRepository<Espacio> _repository;

        public EspacioLogic(IRepository<Espacio> repository)
        {
            _repository = repository;
        }

    }
}
