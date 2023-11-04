using Excepcion;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Objetivo
    {
        public int Id { get; set; }
		public int EspacioId { get; set; }
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
                    throw new DomainEspacioException("El titulo es requerido");
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
					throw new DomainEspacioException("El monto máximo debe ser mayor a 0.");
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
                if (value == null || value.Count == 0 )
					throw new DomainEspacioException("Debe seleccionar al menos una categoría.");
				_categorias = value;
            }
        }

        public bool ContieneCategoria(Categoria categoria)
        {
			return _categorias.Contains(categoria);
		}
    }
}
