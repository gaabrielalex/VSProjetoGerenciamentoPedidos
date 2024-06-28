using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOGerenciamentoPedidos.Src.Data_Base
{
	public class ParametroBDFactory
	{
		private readonly List<ParametroBD> _listaParametros;

		public ParametroBDFactory()
		{
			_listaParametros = new List<ParametroBD>();
		}

		public ParametroBDFactory Adicionar(string nomeParametro, object valorParametro)
		{
			_listaParametros.Add(new ParametroBD(nomeParametro, valorParametro));
			return this;
		}

		public List<ParametroBD> ObterParametros()
		{
			return new List<ParametroBD>(_listaParametros);
		}
	}
}
