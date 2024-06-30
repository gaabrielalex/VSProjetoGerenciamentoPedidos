using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOGerenciamentoPedidos.Src.Data_Base
{
	public class ParametroBD
	{
		public string Nome { get; set; }
		public object Valor { get; set; }

		public ParametroBD() { }
		public ParametroBD(string parametro, object valor)
		{
			Nome = parametro;
			Valor = valor;
		}
	}
}
