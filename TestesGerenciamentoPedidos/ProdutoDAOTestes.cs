using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebGerenciamenttoPedidos.src.dao;
using WebGerenciamenttoPedidos.src.models;

namespace TestesGerenciamentoPedidos
{
	[TestClass]
	public class ProdutoDAOTestes
	{
		[TestMethod]
		public void AoRealizarInsercaoDeveRetonarUmNumeroInteiro()
		{
			// Arrange
			Produto produto = new Produto(null, "Produto Teste", 10.0M);
			ProdutoDAO produtoDAO = new ProdutoDAO();
			int idProduto = 0;

			// Act
			try {
				idProduto = produtoDAO.inserir(produto);
			} catch (Exception e) {

				Assert.Fail("Erro ao inserir produto: " + e.Message);
			}

			// Assert
			if(idProduto <= 0)
			{
				Assert.Fail("Erro ao inserir produto");
			}
		}
	}
}
