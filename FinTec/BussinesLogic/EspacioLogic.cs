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

        public Espacio AddEspacio(Espacio oneElement)
        {
            IList<Espacio> espacios = _repository.FindAll();
            bool existe = espacios.Contains(oneElement);
            if (existe)
            {
                throw new BusinessLogicEspacioException("El espacio ya existe");
            }
            _repository.Add(oneElement);
            return oneElement;
        }
    }
}
