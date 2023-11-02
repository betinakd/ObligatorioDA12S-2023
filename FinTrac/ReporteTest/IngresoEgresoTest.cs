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
	}
}
