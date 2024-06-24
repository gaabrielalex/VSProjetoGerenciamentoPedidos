using DAOGerenciamentoPedidos.Src.Data_Base;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilsGerenciamentoPedidos;

namespace DAOGerenciamentoPedidos.Src
{
	public class MetodoPagamentoDAO : IDAO<MetodoPagamento>
	{
		public MetodoPagamentoDAO() { }

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

		public List<MetodoPagamento> ListarTodos()
		{
			String query = "SELECT * FROM metodo_pagto ORDER BY id_metodo_pagto";
			List<MetodoPagamento> listaMetodoPagto = new List<MetodoPagamento>();
			try
			{
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					SqlDataReader reader = command.ExecuteReader();
					listaMetodoPagto = ConverterReaderParaListaDeObjetos(reader);
					connection.Close();
					return listaMetodoPagto;
				}
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

		public List<MetodoPagamento> ConverterReaderParaListaDeObjetos(SqlDataReader reader)
		{
			List<MetodoPagamento> listaMetodoPagto = new List<MetodoPagamento>();

			// Obtendo os índices das colunas uma vez antes do loop
			int idIndex = reader.GetOrdinal("id_metodo_pagto");
			int descricaoIndex = reader.GetOrdinal("descricao");

			while (reader.Read())
			{
				MetodoPagamento metodoPagamento = new MetodoPagamento(
					idMetodoPagto: reader.GetInt32(idIndex),
					descricao: reader.GetString(descricaoIndex)
				);
				listaMetodoPagto.Add(metodoPagamento);
			}

			return listaMetodoPagto;
		}
	}
}
