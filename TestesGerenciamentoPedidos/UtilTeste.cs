using DAOGerenciamentoPedidos.Src.Data_Base;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using WebGereciamentoPedidos.src.util;

namespace TestesGerenciamentoPedidos
{
	[TestClass]
	public class UtilTeste
	{

		[TestMethod]
		public void ApagarTodosOsRegristosTesteDoBanco()
		{
			try
			{
				
				using (SqlConnection con = DB_Connection.getConnection())
				{
					string sql = "DELETE FROM pedido WHERE nome_cliente LIKE '%Teste%'";
					con.Open();
					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.ExecuteNonQuery();
					con.Close();
				}
				using (SqlConnection con = DB_Connection.getConnection())
				{
					string sql = "DELETE FROM item_pedido WHERE id_pedido IN (SELECT id_pedido FROM pedido WHERE nome_cliente LIKE '%Teste%')";
					con.Open();
					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.ExecuteNonQuery();
					con.Close();
				}
				using (SqlConnection con = DB_Connection.getConnection())
				{
					string sql = "DELETE FROM metodo_pagto WHERE descricao LIKE '%Teste%'";
					con.Open();
					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.ExecuteNonQuery();
					con.Close();
				}
				using (SqlConnection con = DB_Connection.getConnection())
				{
					string sql = "DELETE FROM produto WHERE descricao LIKE '%Teste%'";
					con.Open();
					SqlCommand cmd = new SqlCommand(sql, con);
					cmd.ExecuteNonQuery();
					con.Close();
				}
			}
			catch (Exception e)
			{
				e.Should().BeNull(because: "Não deve ocorrer erro ao apagar registros de teste do banco");
			}
		}
	}
}
