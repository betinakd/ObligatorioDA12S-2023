﻿using System.Text.RegularExpressions;
using Domain.DomainExceptions;

namespace Domain
{
    public class Usuario
    {
        public string Contrasena { get; set; }
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

        private static bool ContienePuntoCom(string correo)
        {
            return Regex.IsMatch(correo, _patron);
        }

        private static bool ContieneArroba(string correo)
        {
            return correo.Contains("@");
        }

        private static bool SonTodasMinusculas(string contrasena)
        {
            return contrasena.ToLower() == contrasena;
        }

        private static bool EsContrasenaMayorIgualADiez(string contrasena)
        {
            return contrasena.Length >= 10;
        }

        private static bool EsContrasenaMayorATreinta(string contrasena)
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
