using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ModelsGerenciamentoPedidos.Src;
using DAOGerenciamentoPedidos.Src.Data_Base;

namespace DAOGerenciamentoPedidos.Src
{
	public class ProdutoDAO
	{ 
		public ProdutoDAO() { }

		public int inserir(Produto produto)
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
				throw new Exception("Erro ao inserir produto: " + e.Message);
			}

		}

		public bool editar(Produto produto, int idProduto)
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
					return linhasAfetadas > 0;
				}
			}
			catch (Exception e)
			{
				throw new Exception("Erro ao editar produto: " + e.Message);
			}
		}

		public bool excluir(int idProduto)
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
					return linhasAfetadas > 0;
				}
			}
			catch (Exception e)
			{
				throw new Exception("Erro ao excluir produto: " + e.Message);
			}
		}

		public List<Produto> listar()
		{
			//Query de listagem
			String query = "SELECT id_produto, descricao, vlr_unitario FROM produto";
			List<Produto> produtos = new List<Produto>();
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						Produto produto = new Produto(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2));
						produtos.Add(produto);
					}
					connection.Close();
					return produtos;
				}
			}
			catch (Exception e)
			{
				throw new Exception("Erro ao listar produtos: " + e.Message);
			}
		}

		public List<Produto> listarPorDescricao(String descricao)
		{
			//Query de listagem
			String query = "SELECT id_produto, descricao, vlr_unitario FROM produto WHERE descricao LIKE @descricao";
			List<Produto> produtos = new List<Produto>();
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@descricao", "%" + descricao + "%");
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						Produto produto = new Produto(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2));
						produtos.Add(produto);
					}
					connection.Close();
					return produtos;
				}
			}
			catch (Exception e)
			{
				throw new Exception("Erro ao listar produtos: " + e.Message);
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
				throw new Exception("Erro ao realizar verificação da já existência do produto: " + e.Message);
			}
		}
	}
}