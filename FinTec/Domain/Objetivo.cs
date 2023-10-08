using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Objetivo
    {
        private string _titulo;
        private double _montoMaximo;
        private List<Categoria> _categorias = new List<Categoria>();
        public string Titulo 
        {
            get
            { 
                return _titulo;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("El titulo es requerido");
                _titulo = value;
            }
        }
        public double MontoMaximo
        {
			get
            {
				return _montoMaximo;
			}
			set
            {
				if (value <= 0)
					throw new DomainObjetivoException("El monto máximo debe ser mayor a 0.");
				_montoMaximo = value;
			}
		}
        public List<Categoria> Categorias { 
            get
            {
				return _categorias;
			}
            set
            {
                if (value.Count == 0)
					throw new DomainObjetivoException("Debe seleccionar al menos una categoría.");
				_categorias = value;
            }
        }
    }
}
