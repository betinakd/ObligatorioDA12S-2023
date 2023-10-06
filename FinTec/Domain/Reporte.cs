using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Reporte
    {
        private int _contadorId = 1;
        private int _id;
        private Espacio? _MiEspacio;

        public Espacio MiEspacio
        {
            set { _MiEspacio = value; }
            get { return _MiEspacio; }
        }
    }
}
