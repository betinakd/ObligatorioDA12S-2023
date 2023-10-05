using System.Text.RegularExpressions;
using Domain.DomainExceptions;

namespace Domain
{
    public class Usuario
    {
        private string _contrasena;
		public string Contrasena
		{
			get
			{
				return _contrasena;
			}
			set
			{
				if (!Validar_Contrasena(value))
				{
					throw new DomainUsuarioException("La contraseña no es válida");
				}
				_contrasena = value;
			}
		}
		public string Correo { get; set; }
        private string _nombre;
        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new DomainUsuarioException("El nombre es requerido");
                }
                _nombre = value;
            }
        }

        private string _apellido;
        public string Apellido
        {
            get
            {
                return _apellido;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new DomainUsuarioException("El apellido es requerido");
                }
                _apellido = value;
            }
        }

        private const string _patron = @".+\.com$";

        public bool Validar_Contrasena(string contrasena)
        {
            if (EsContrasenaMayorATreinta(contrasena))
            {
                return false;
            }

            if (SonTodasMinusculas(contrasena))
            {
                return false;
            } 

            return EsContrasenaMayorIgualADiez(contrasena);
        }
        public bool Validar_Correo(string correo)
        {
            if (!ContienePuntoCom(correo))
            {
                return false;
            }
            return ContieneArroba(correo);
        }

        private bool ContienePuntoCom(string correo)
        {
            return Regex.IsMatch(correo, _patron);
        }

        private bool ContieneArroba(string correo)
        {
            return correo.Contains("@");
        }

        private  bool SonTodasMinusculas(string contrasena)
        {
            return contrasena.ToLower() == contrasena;
        }

        private bool EsContrasenaMayorIgualADiez(string contrasena)
        {
            return contrasena.Length >= 10;
        }

        private bool EsContrasenaMayorATreinta(string contrasena)
        {
            return contrasena.Length > 30;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            };

            Usuario user = (Usuario)obj;
            return Correo == user.Correo;
        }
    }


}
