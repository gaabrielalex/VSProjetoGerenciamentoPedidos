using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebGerenciamenttoPedidos.src.models;
using WebGerenciamenttoPedidos.src.utils;

namespace WebGerenciamenttoPedidos.src.dao
{
	public class ProdutoDAO
	{
		public ProdutoDAO() { }

		public int inserir(Produto produto) 
		{	
			//Query da inserção
			String query = @"INSERT INTO produto (descricao, vlr_unitario) VALUES (@descricao, @vlr_unitario);
						SELECT SCOPE_IDENTITY();";
			try {

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

			} catch (Exception e) {
				throw new Exception("Erro ao inserir produto: " + e.Message);
			}

		}

		public bool editar(Produto produto, int idProdutoEditado)
		{
			//Query de edição
			String query = @"UPDATE produto SET descricao = @descricao, vlr_unitario = @vlr_unitario WHERE id_produto = @id_produto";
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@descricao", produto.Descricao);
					command.Parameters.AddWithValue("@vlr_unitario", produto.VlrUnitario);
					command.Parameters.AddWithValue("@id_produto", idProdutoEditado);
					command.ExecuteNonQuery();
					connection.Close();
					return true;
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
			String query = @"DELETE FROM produto WHERE id_produto = @id_produto";
			try
			{
				//Obtendo conexão co banco
				using (SqlConnection connection = DB_Connection.getConnection())
				{
					connection.Open();
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@id_produto", idProduto);
					command.ExecuteNonQuery();
					connection.Close();
					return true;
				}
			}
			catch (Exception e)
			{
				throw new Exception("Erro ao excluir produto: " + e.Message);
			}
		}

	}
}