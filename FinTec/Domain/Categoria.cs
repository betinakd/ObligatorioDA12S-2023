using System.Security.Cryptography.X509Certificates;

namespace Domain
{
	public class Categoria
	{
		private string _nombre;
		private readonly DateTime _fechaCreacion = DateTime.Now;
		public bool EstadoActivo { get; set; }
		public TipoCategoria Tipo { get; set; }
		public string Nombre
		{
			get { return _nombre; }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new DomainCategoriaException("El nombre de la categoría no puede ser vacío");
				}
				_nombre = value;
			}
		}

		public DateTime FechaCreacion
		{
			get { return _fechaCreacion; }
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
			return cat.EstadoActivo == EstadoActivo && cat.FechaCreacion == FechaCreacion && cat.Tipo == Tipo && cat.Nombre == Nombre;
		}
	}

	public enum TipoCategoria
	{
		Costo,
		Ingreso
	}
}
