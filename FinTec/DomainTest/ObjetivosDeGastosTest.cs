﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainTest
{
    [TestClass]
    public class ObjetivosDeGastosTest
    {
        [TestMethod]
        public void Nuevo_Objetivo()
        {
            var objetivo = new ObjetivosDeGastos();
            Assert.IsNotNull(objetivo);
        }

        [TestMethod]
        public void Tiene_Titulo()
        {
            var objetivo = new ObjetivosDeGastos();
            objetivo.Titulo = "Objetivo 1";
            Assert.AreEqual("Objetivo 1", objetivo.Titulo);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Tiene_Titulo_Vacio()
        {
            var objetivo = new ObjetivosDeGastos();
            objetivo.Titulo = "";
        }

        [TestMethod]
        public void Monto_Maximo()
        {
            var objetivo = new ObjetivosDeGastos();
            objetivo.MontoMaximo = 1000;
            Assert.AreEqual(1000, objetivo.MontoMaximo);
        }

        [TestMethod]
        public void  Aplicar_Ojetivo_Categoria()
        {
            var objetivo = new ObjetivosDeGastos();
            var categoria = new Categoria();
            var categorias = new List<Categoria>();
            categoria.EstadoActivo=true;
            categoria.Tipo=TipoCategoria.Ingreso;
            categoria.Nombre="Sueldo";
            categorias.Add(categoria);
            objetivo.Categorias=categorias;
            var resultado = objetivo.Categorias;
            Assert.AreEqual(categorias, resultado);
        }
    }
}
