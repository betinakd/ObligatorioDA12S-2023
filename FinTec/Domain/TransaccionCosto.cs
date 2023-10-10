using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TransaccionCosto : Transaccion
    {
        private Categoria _categoria;
        public override Categoria CategoriaTransaccion
        {
            get
            {
                return _categoria;
            }
            set
            {
                if (value == null)
                    throw new DomainEspacioException("La categoria no puede ser nula");
                if (value.EstadoActivo == false)
                    throw new DomainEspacioException("La categoria tiene que estar activa");
                if (value.Tipo != TipoCategoria.Costo)
                    throw new DomainEspacioException("La categoria tiene que ser de tipo Costo");
                _categoria = value;
            }
        }

    }
}
