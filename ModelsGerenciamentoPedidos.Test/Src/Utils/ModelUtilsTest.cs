using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelsGerenciamentoPedidos.Src.Utils;
using System;

namespace TestesGerenciamentoPedidos
{
	[TestClass]
	public class ModelUtilsTest
	{
		[TestMethod]
		public void AoObterQuantidadeDeDigitosAntesDoSeparadorDecimalDeUmValor3DigitosE2CasasDecimaisDeveRetornarTres()
		{
			// Arrange
			decimal valor = 123.45m;
			// Act
			int quantidadeDigitos = ModelUtils.ObterQuantidadeDeDigitosAntesDoSeparadorDecimal(valor);
			// Assert
			int quantidadeDigitosEsperada = 3;
			quantidadeDigitos.Should().Be(quantidadeDigitosEsperada, because: "O valor informado possui 3 digitos antes do separador decimal");
		}

		[TestMethod]
		public void AoObterQuantidadeDeDigitosAntesDoSeparadorDecimalDeUmValorCom2DigitosENennhumaCasaDecimalDeveRetornar2()
		{
			// Arrange
			decimal valor = 12M;
			// Act
			int quantidadeDigitos = ModelUtils.ObterQuantidadeDeDigitosAntesDoSeparadorDecimal(valor);
			// Assert
			int quantidadeDigitosEsperada = 2;
			quantidadeDigitos.Should().Be(quantidadeDigitosEsperada, because: "O valor informado possui 2 digitos antes do separador decimal");
		}
		
	}
}
