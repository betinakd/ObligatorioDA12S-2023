using System.Security.Cryptography.X509Certificates;

namespace Domain
{
	public class Categoria
	{
		private string _nombre;
		private readonly DateTime _fechaCreacion = DateTime.Now;
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
	}
}
