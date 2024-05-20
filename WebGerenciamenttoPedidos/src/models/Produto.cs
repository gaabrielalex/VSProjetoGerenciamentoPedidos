using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGerenciamenttoPedidos.src.models
{
	public class Produto
	{
		public int IdPproduto { get; set; }
		public String Descricao { get; set; }
		
		public decimal VlrUnitario { get; set; }

		public Produto(int idProduto, String descricao, decimal vlrUnitario) {
			IdPproduto = idProduto;
			Descricao = descricao;
			VlrUnitario = vlrUnitario;
		}
	}
}