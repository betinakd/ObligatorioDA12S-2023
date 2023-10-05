using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EspacioMemoryRepository : IRepository<Espacio>
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

        public IList<Espacio> FindAll()
        {
            return _espacios;
        }

        public Espacio? Update(Espacio updateEntity)
        {
            var espacio = Find(u => u.Admin == updateEntity.Admin);
            if (espacio != null)
            {               
                espacio.Admin = updateEntity.Admin;
            }
            return espacio;
        }

        public void Delete(string id)
        {
            var espacio = Find(u => u.Admin.Correo == id);
            if (espacio != null)
            {
                _espacios.Remove(espacio);
            }
        }
    }
}
