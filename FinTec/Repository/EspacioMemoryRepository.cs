using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EspacioMemoryRepository
    {
        private readonly List<Espacio> _espacios = new List<Espacio>();
        public Espacio Add(Espacio oneElement)
        {
            _espacios.Add(oneElement);
            return oneElement;
        }

        public Espacio? Find(Func<Espacio, bool> filter)
        {
            return _espacios.FirstOrDefault(filter);
        }
    }
}
