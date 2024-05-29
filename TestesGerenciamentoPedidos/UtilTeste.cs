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
		private readonly string[] TABELAS_BD = { "item_pedido", "pedido", "produto", "metodo_pagto" };

		[TestMethod]
		public void ApagarTodosOsRegristosTesteDoBanco()
		{
			try
			{
				foreach(string tabela in TABELAS_BD)
				{
					using (SqlConnection con = DB_Connection.getConnection())
					{
						string sql = "DELETE FROM produto WHERE descricao LIKE '%Teste%'";
						con.Open();
						SqlCommand cmd = new SqlCommand(sql, con);
						cmd.ExecuteNonQuery();
						con.Close();
					}
				}
			}
			catch (Exception e)
			{
				e.Should().BeNull(because: "Não deve ocorrer erro ao apagar registros de teste do banco");
			}
		}
	}
}
