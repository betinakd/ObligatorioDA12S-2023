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
            
            _repository.Add(oneElement);
            return oneElement;
        }

        public void DeleteEspacio(Espacio oneElement)
        {
            _repository.Delete(oneElement.Admin.Correo);
        }

        public IList<Espacio> FindAllEspacios()
        {
            return _repository.FindAll();
        }

        public Espacio? FindEspacio(string id)
        {
            return _repository.Find(u => u.Admin.Correo == id);
        }
    }
}
