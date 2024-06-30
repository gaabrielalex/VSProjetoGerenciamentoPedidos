using DAOGerenciamentoPedidos.Src.Data_Base;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsGerenciamentoPedidos;

namespace DAOGerenciamentoPedidos.Src
{
	public class MetodoPagamentoDAO : IDAO<MetodoPagamento>
	{
		private readonly BancoDeDados _bancoDeDados;
		
		public MetodoPagamentoDAO(BancoDeDados bancoDeDados)
		{
			_bancoDeDados = bancoDeDados;
		}

		public void Editar(MetodoPagamento obj, int idObjASerEditado)
		{
			throw new NotImplementedException();
		}

		public void Excluir(int id)
		{
			throw new NotImplementedException();
		}

		public int Inserir(MetodoPagamento obj)
		{
			throw new NotImplementedException();
		}

		public IList<MetodoPagamento> ListarTodos()
		{
			var query = "SELECT * FROM metodo_pagto ORDER BY id_metodo_pagto";
			try
			{
				var listaMetodoPagto = ConverterReaderParaListaDeObjetos(_bancoDeDados.ConsultarReader(query));
				return listaMetodoPagto;
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao listar métodos de pagamento: {e.ToString()}");
			}
		}

		public MetodoPagamento ObterPorId(int id)
		{
			throw new NotImplementedException();
		}

		public IList<MetodoPagamento> ConverterReaderParaListaDeObjetos(IEnumerable<IDataRecord> reader)
		{
			List<MetodoPagamento> listaMetodoPagto = new List<MetodoPagamento>();

			foreach (var record in reader)
			{
				MetodoPagamento metodoPagamento = new MetodoPagamento(
					idMetodoPagto: record.GetInt32(record.GetOrdinal("id_metodo_pagto")),
					descricao: record.GetString(record.GetOrdinal("descricao"))
				);
				listaMetodoPagto.Add(metodoPagamento);
			}

			return listaMetodoPagto;
		}
	}
}
