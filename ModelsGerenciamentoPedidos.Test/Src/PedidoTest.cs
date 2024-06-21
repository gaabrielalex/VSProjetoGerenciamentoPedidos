using FluentAssertions;
using FluentAssertions.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelsGerenciamentoPedidos.Src;
using System;

namespace TestesGerenciamentoPedidos
{
	[TestClass]
	public class PedidoTest
	{
		[TestMethod]
		public void AoCriarPedidoComNomeDoClienteMaiorQue100CaracteresDeveRetornarExcecao()
		{
			// Arrange
			const int tamanhoString = 101;
			string nomeCliente = "".PadRight(tamanhoString, 'a');
			Pedido pedido = new Pedido();
			// Act
			Action act = () => pedido.NomeCliente = nomeCliente;
			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>(because: "O tamanho do nome do cliente deve ser menor ou igual a 100 caracteres. O valor informado foi: " + tamanhoString + " caracteres.");
		}

		[TestMethod]
		public void AoCriarPedidoComValorTotalMenorQueZeroDeveRetornarExcecao()
		{
			// Arrange
			decimal valorTotal = -1;
			Pedido pedido = new Pedido();
			// Act
			Action act = () => pedido.VlrSubtotal = valorTotal;
			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>(because: "O valor total do pedido deve ser maior ou igual a zero. O valor informado foi: " + valorTotal);
		}

		[TestMethod]
		public void AoCriarPedidoComValorTotalMaiorQue7DigitosNaParteInteiraDeveRetornarExcecao()
		{
			// Arrange
			decimal valorTotal = 10000000;
			Pedido pedido = new Pedido();
			// Act
			Action act = () => pedido.VlrSubtotal = valorTotal;
			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>(because: "O valor total do pedido deve ter no máximo 7 dígitos na parte inteira. O valor informado foi: " + valorTotal.ToString().Length + " dígitos na parte inteira.");
		}

		[TestMethod]
		public void AoCriarPedidoComDescontoMenorQueZeroDeveRetornarExcecao()
		{
			// Arrange
			decimal desconto = -1;
			Pedido pedido = new Pedido();
			// Act
			Action act = () => pedido.Desconto = desconto;
			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>(because: "O desconto do pedido deve ser maior ou igual a zero. O valor informado foi: " + desconto);
		}

		[TestMethod]
		public void AoCriarPedidoComDescontoMaiorQue6DigitosNaParteInteiraDeveRetornarExcecao()
		{
			// Arrange
			decimal desconto = 1000000;
			Pedido pedido = new Pedido();
			// Act
			Action act = () => pedido.Desconto = desconto;
			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>(because: "O desconto do pedido deve ter no máximo 6 dígitos na parte inteira. O valor informado foi: " + desconto.ToString().Length + " dígitos na parte inteira.");
		}

		[TestMethod]
		public void AoCriarPedidoComObservacoesMaiorQue200CaracteresDeveRetornarExcecao()
		{
			// Arrange
			const int tamanhoString = 201;
			string observacoes = "".PadRight(tamanhoString, 'a');
			Pedido pedido = new Pedido();
			// Act
			Action act = () => pedido.Observacoes = observacoes;
			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>(because: "O tamanho das observações do pedido deve ser menor ou igual a 200 caracteres. O valor informado foi: " + tamanhoString + " caracteres.");
		}

	}
}
