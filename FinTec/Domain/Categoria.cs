using System.Security.Cryptography.X509Certificates;

namespace Domain
{
	public class Categoria
	{
		private string _nombre;
		DateTime _fechaCreacion = DateTime.Today;
		public bool EstadoActivo { get; set; }
		public TipoCategoria Tipo { get; set; }
		public string Nombre
		{
			get { return _nombre; }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new DomainEspacioException("El nombre de la categoría no puede ser vacío");
				}
				_nombre = value;
			}
		}

		public DateTime FechaCreacion
		{
			get
			{
				return _fechaCreacion;
			}
			set
			{
				_fechaCreacion = value;
			}
		}

		public Categoria()
		{
		}

		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			Categoria cat = (Categoria)obj;
			return cat.Nombre == Nombre;
		}
	}

	public enum TipoCategoria
	{
		Costo,
		Ingreso
	}
}
