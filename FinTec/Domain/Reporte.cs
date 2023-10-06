using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Reporte
    {
        private Usuario _user;
        public Espacio _MiEspacio { set; get; }

        public Usuario User { get { return _user; } set { _user = value; } }

        /*public Espacio MiEspacio
        {
            set { _MiEspacio = value; }
            get { return _MiEspacio; }
        }*/
    }
}
