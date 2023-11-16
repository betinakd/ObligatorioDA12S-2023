using EspacioReporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspacioReporteTest
{
	[TestClass]
	public class IngresoEgresoTest
	{
		[TestMethod]
		public void IngresoEgreso_No_Vacio() 
		{
			IngresoEgreso ingresoEgreso = new IngresoEgreso();
			Assert.IsNotNull(ingresoEgreso);
		}

		[TestMethod]
		public void IngresoEgreso_Fecha_Hoy()
		{
			IngresoEgreso ingresoEgreso = new IngresoEgreso();
			Assert.IsFalse(ingresoEgreso.Fecha != DateTime.Today);
		}

		[TestMethod]
		public void IngresoEgreso_Valores_En_Cero()
		{
			IngresoEgreso ingresoEgreso = new IngresoEgreso();
			Assert.IsTrue(ingresoEgreso.Ingresos == 0 && ingresoEgreso.Egresos == 0);
		}

		[TestMethod]
		public void IngresoEgreso_Fecha_Bien_Definida()
		{
			double ingresos = 0;
			double egresos = 0;
			DateTime fecha = new DateTime(2023, 11, 2);
			IngresoEgreso ingresoEgreso = new IngresoEgreso(fecha, ingresos, egresos);
			Assert.IsTrue(ingresoEgreso.Fecha.Equals(fecha));
		}

		[TestMethod]
		public void IngresoEgreso_Valores_Bien_Definidos()
		{
			double ingresos = 200;
			double egresos = 500;
			DateTime fecha = new DateTime(2023, 11, 2);
			IngresoEgreso ingresoEgreso = new IngresoEgreso(fecha, ingresos, egresos);
			Assert.IsTrue(ingresoEgreso.Ingresos.Equals(ingresos) && ingresoEgreso.Egresos.Equals(egresos));
		}
	}
}
