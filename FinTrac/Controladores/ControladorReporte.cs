using BussinesLogic;
using DTO;
using DTO.EnumsDTO;
using Domain;
using Excepcion;
using EspacioReporte;
using System;

namespace Controlador
{
	public class ControladorReporte
	{
		private EspacioLogic _reporte;

		public ControladorReporte(EspacioLogic reporte)
		{
			_reporte = reporte;
		}

		public List<ObjetivoGastoDTO> ReporteObjetivosGastos(int id)
		{
			Espacio espacio = _reporte.FindEspacio(id);
			Reporte reporte = new Reporte(espacio);
			List<ObjetivoGasto> reporteObjetivoGasto = reporte.ReporteObjetivosDeGastos();
			List<ObjetivoGastoDTO> reporteEnDTO = new List<ObjetivoGastoDTO>();
			foreach (ObjetivoGasto objetivoGasto in reporteObjetivoGasto)
			{
				ObjetivoGastoDTO obj = new ObjetivoGastoDTO()
				{
					MontoAcumulado = objetivoGasto.MontoAcumulado,
					MontoEsperado = objetivoGasto.MontoEsperado,
					Objetivo = new ObjetivoDTO()
					{
						Categorias = CategoriasA_DTO(objetivoGasto.Objetivo.Categorias),
						Id = objetivoGasto.Objetivo.Id,
						MontoMaximo = objetivoGasto.Objetivo.MontoMaximo,
						Titulo = objetivoGasto.Objetivo.Titulo,
						Token = objetivoGasto.Objetivo.Token,
					}
				};
				reporteEnDTO.Add(obj);
			}
			return reporteEnDTO;
		}

		


		private Ahorro AhorroDTO_A_Ahorro(AhorroDTO account)
		{
			Ahorro cuenta;
			if (account.Moneda.Equals(TipoCambiarioDTO.Euro))
			{
				cuenta = new Ahorro()
				{
					FechaCreacion = account.FechaCreacion,
					Moneda = TipoCambiario.Euro,
					Monto = account.Monto,
					Nombre = account.Nombre,
				};
			}
			else if (account.Moneda.Equals(TipoCambiarioDTO.Dolar))
			{
				cuenta = new Ahorro()
				{
					FechaCreacion = account.FechaCreacion,
					Moneda = TipoCambiario.Dolar,
					Monto = account.Monto,
					Nombre = account.Nombre,
				};
			}
			else
			{
				cuenta = new Ahorro()
				{
					FechaCreacion = account.FechaCreacion,
					Moneda = TipoCambiario.PesosUruguayos,
					Monto = account.Monto,
					Nombre = account.Nombre,
				};
			}
			return cuenta;
		}

		private Credito CreditoDTO_A_Credito(CreditoDTO credit)
		{
			Credito cuenta;
			if (credit.Moneda.Equals(TipoCambiarioDTO.Euro))
			{
				cuenta = new Credito()
				{
					NumeroTarjeta = credit.NumeroTarjeta,
					BancoEmisor = credit.BancoEmisor,
					CreditoDisponible = credit.CreditoDisponible,
					FechaCierre = credit.FechaCierre,
					Moneda = TipoCambiario.Euro,
				};
			}
			else if (credit.Moneda.Equals(TipoCambiarioDTO.Dolar))
			{
				cuenta = new Credito()
				{
					NumeroTarjeta = credit.NumeroTarjeta,
					BancoEmisor = credit.BancoEmisor,
					CreditoDisponible = credit.CreditoDisponible,
					FechaCierre = credit.FechaCierre,
					Moneda = TipoCambiario.Dolar,
				};
			}
			else
			{
				cuenta = new Credito()
				{
					NumeroTarjeta = credit.NumeroTarjeta,
					BancoEmisor = credit.BancoEmisor,
					CreditoDisponible = credit.CreditoDisponible,
					FechaCierre = credit.FechaCierre,
					Moneda = TipoCambiario.PesosUruguayos,
				};
			}
			return cuenta;
		}

		private List<CategoriaDTO> CategoriasA_DTO(List<Categoria> categorias)
		{
			List<CategoriaDTO> categoriaDTOs = new List<CategoriaDTO>();
			foreach (Categoria cat in categorias)
			{
				CategoriaDTO categoria = new CategoriaDTO()
				{
					Id = cat.Id,
					EstadoActivo = cat.EstadoActivo,
					FechaCreacion = cat.FechaCreacion,
					Nombre = cat.Nombre,
					Tipo = TipoCategoriaDTO.Costo,
				};
				categoriaDTOs.Add(categoria);
			}
			return categoriaDTOs;
		}

		private Categoria CategoriaDTO_A_Categoria(CategoriaDTO categoria)
		{
			Categoria categoriaRetornar;
			if (categoria.Tipo.Equals(TipoCategoriaDTO.Costo))
			{
				categoriaRetornar = new Categoria()
				{
					EstadoActivo = categoria.EstadoActivo,
					FechaCreacion = categoria.FechaCreacion,
					Id = categoria.Id,
					Nombre = categoria.Nombre,
					Tipo = TipoCategoria.Costo,
				};
			}
			else
			{
				categoriaRetornar = new Categoria()
				{
					EstadoActivo = categoria.EstadoActivo,
					FechaCreacion = categoria.FechaCreacion,
					Id = categoria.Id,
					Nombre = categoria.Nombre,
					Tipo = TipoCategoria.Ingreso,
				};
			}
			return categoriaRetornar;
		}

	}
}
