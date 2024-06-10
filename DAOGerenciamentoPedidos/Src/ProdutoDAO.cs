using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ModelsGerenciamentoPedidos.Src;
using DAOGerenciamentoPedidos.Src.Data_Base;
using UtilsGerenciamentoPedidos;
using System.Runtime.Remoting.Messaging;

namespace DAOGerenciamentoPedidos.Src
{
	public class ProdutoDAO : IDAO<Produto>
	{ 
		public ProdutoDAO() { }

		public int Inserir(Produto produto)
		{
			//Query da inserção
			String query = @"INSERT INTO produto (descricao, vlr_unitario) VALUES (@descricao, @vlr_unitario);
							SELECT SCOPE_IDENTITY();";
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@descricao", produto.Descricao);
					command.Parameters.AddWithValue("@vlr_unitario", produto.VlrUnitario);
					int idProduto = Convert.ToInt32(command.ExecuteScalar());
					connection.Close();
					return idProduto;
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao inserir produto: {e.ToString()}");
			}
		}

		public void Editar(Produto produto, int idProduto)
		{
			//Query de edição
			String query = "UPDATE produto SET descricao = @descricao, vlr_unitario = @vlr_unitario WHERE id_produto = @id_produto";
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@descricao", produto.Descricao);
					command.Parameters.AddWithValue("@vlr_unitario", produto.VlrUnitario);
					command.Parameters.AddWithValue("@id_produto", idProduto);
					var linhasAfetadas = command.ExecuteNonQuery();
					connection.Close();
					if(linhasAfetadas < 0) {
						RegistroLog.Log("Erro ao editar produto: Nenhuma linha foi afetada - Id: " + idProduto );
						throw new Exception("Erro ao editar produto: Nenhuma linha foi afetada");
					}
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao editar produto: {e.ToString()}");
			}
		}

		public void Excluir(int idProduto)
		{
			//Query de exclusão
			String query = "DELETE FROM produto WHERE id_produto = @id_produto";
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@id_produto", idProduto);
					var linhasAfetadas = command.ExecuteNonQuery();
					connection.Close();
					if(linhasAfetadas < 0) {
						RegistroLog.Log("Erro ao excluir produto: Nenhuma linha foi afetada - Id: " + idProduto );
						throw new Exception("Erro ao excluir produto: Nenhuma linha foi afetada");
					}
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao excluir produto: {e.ToString()}");
			}
		}

		public List<Produto> Listar()
		{
			//Query de listagem
			String query = "SELECT * FROM produto";
			List<Produto> listaProdutos = new List<Produto>();
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					SqlDataReader reader = command.ExecuteReader();
					listaProdutos = ConverterReaderParaListaDeObjetos(reader);
					connection.Close();
					return listaProdutos;
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao listar produtos: {e.ToString()}");
			}
		}

		public List<Produto> ListarPorDescricao(String descricao)
		{
			//Query de listagem
			String query = "SELECT * FROM produto WHERE descricao LIKE @descricao";
			List<Produto> listaProdutos = new List<Produto>();
			try
			{
				//Obtendo conexão com banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@descricao", "%" + descricao + "%");
					SqlDataReader reader = command.ExecuteReader();
					listaProdutos = ConverterReaderParaListaDeObjetos(reader);
					connection.Close();
					return listaProdutos;
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao listar produtos: {e.ToString()}");
			}
		}

		public Produto ObterPorId(int idProduto)
		{
			String query = "SELECT * FROM produto where id_produto = @id_produto";
			List<Produto> listaProdutos = new List<Produto>();
			try
			{
				using(SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@id_produto", idProduto);
					SqlDataReader reader = command.ExecuteReader();
					listaProdutos = ConverterReaderParaListaDeObjetos(reader);
					connection.Close();
					return listaProdutos[0];
				}
			} catch (Exception e) 
			{
				throw new Erro($"Erro ao obter produto: {e.ToString()}");
			}
		}

		public bool DescricaoJaExiste(String descricao) {
			//Obs.: COLLATE Latin1_General_CI_A -- Determina que a comparação será incanse sentive e que será ignorado acentuações
			String query = "SELECT * FROM produto WHERE descricao COLLATE Latin1_General_CI_AI = @descricao";
			List<Produto> produtos = new List<Produto>();
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@descricao", descricao);
					SqlDataReader reader = command.ExecuteReader();
					int quantidadeRegistros = 0;
					while (reader.Read())
					{
						quantidadeRegistros += 1;
					}
					connection.Close();
					return quantidadeRegistros > 0;
				}
			}
			catch (Exception e)
			{
				throw new Erro($"Erro ao realizar verificação da já existência do produto: { e.ToString()}");
			}
		}

		public List<Produto> ConverterReaderParaListaDeObjetos(SqlDataReader reader)
		{
			List<Produto> listaProdutos = new List<Produto>();	

			// Obtendo os índices das colunas uma vez antes do loop
			int idIndex = reader.GetOrdinal("id_produto");
			int descricaoIndex = reader.GetOrdinal("descricao");
			int vlrUnitarioIndex = reader.GetOrdinal("vlr_unitario");

			while (reader.Read())
			{
				Produto produto = new Produto(
					idProduto: reader.GetInt32(idIndex),
					descricao: reader.GetString(descricaoIndex),
					vlrUnitario: reader.GetDecimal(vlrUnitarioIndex)
				);
				listaProdutos.Add(produto);
			}

			return listaProdutos;
		}
	}
}