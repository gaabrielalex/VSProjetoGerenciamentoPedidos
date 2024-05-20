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
	}
}