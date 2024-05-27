using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebGereciamentoPedidos.src.util;

namespace TestesGerenciamentoPedidos
{
	[TestClass]
	public class ModelUtilsTestecs
	{
		[TestMethod]
		public void AoObterQuantidadeDeDigitosAntesDoSeparadorDecimalDeUmValorComDuasCasasDecimaisDeveRetornarDois()
		{
			// Arrange
			decimal valor = 123.45m;
			// Act
			int quantidadeDigitos = ModelUtils.ObterQuantidadeDeDigitosAntesDoSeparadorDecimal(valor);
			// Assert
			int quantidadeDigitosEsperada = 3;
			quantidadeDigitos.Should().Be(quantidadeDigitosEsperada, because: "O valor informado possui 3 digitos antes do separador decimal");
		}
		
	}
}
