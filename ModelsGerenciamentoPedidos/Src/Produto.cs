using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsGerenciamentoPedidos.Src
{
	[Serializable]
	public class Produto
	{
		public int? IdProduto { get; set; }
		public String Descricao { get; set; }

		public decimal VlrUnitario { get; set; }

		public Produto()
		{
		}
		public Produto(int? idProduto, String descricao, decimal vlrUnitario)
		{
			IdProduto = idProduto;
			Descricao = descricao;
			VlrUnitario = vlrUnitario;
		}

		public override bool Equals(object obj)
		{
			return obj is Produto produto &&
				   IdProduto == produto.IdProduto &&
				   Descricao == produto.Descricao &&
				   VlrUnitario == produto.VlrUnitario;
		}

		public override int GetHashCode()
		{
			int hashCode = -974680947;
			hashCode = hashCode * -1521134295 + IdProduto.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Descricao);
			hashCode = hashCode * -1521134295 + VlrUnitario.GetHashCode();
			return hashCode;
		}
	}
}