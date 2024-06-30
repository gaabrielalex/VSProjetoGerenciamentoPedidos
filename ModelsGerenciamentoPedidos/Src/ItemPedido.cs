using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsGerenciamentoPedidos.Src
{
	public class ItemPedido
	{
		public int? IdItemPedido { get; set; }
		public int IdPedido { get; set; }
		public Produto Produto { get; set; }
		public int Quantidade { get; set; }
		public decimal VlrTotalItem { get; set; }

		public ItemPedido()
		{
		}

		public ItemPedido(int? idItemPedido, int idPedido, Produto produto, int quantidade, decimal vlrTotalItem)
		{
			IdItemPedido = idItemPedido;
			IdPedido = idPedido;
			Produto = produto;
			Quantidade = quantidade;
			VlrTotalItem = vlrTotalItem;
		}

		public ItemPedido(int idPedido, Produto produto, int quantidade, decimal vlrTotalItem)
		{
			IdPedido = idPedido;
			Produto = produto;
			Quantidade = quantidade;
			VlrTotalItem = vlrTotalItem;
		}

		public override bool Equals(object obj)
		{
			return obj is ItemPedido pedido &&
				   IdItemPedido == pedido.IdItemPedido &&
				   IdPedido == pedido.IdPedido &&
				   EqualityComparer<Produto>.Default.Equals(Produto, pedido.Produto) &&
				   Quantidade == pedido.Quantidade &&
				   VlrTotalItem == pedido.VlrTotalItem;
		}

		public override int GetHashCode()
		{
			int hashCode = -359571340;
			hashCode = hashCode * -1521134295 + IdItemPedido.GetHashCode();
			hashCode = hashCode * -1521134295 + IdPedido.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<Produto>.Default.GetHashCode(Produto);
			hashCode = hashCode * -1521134295 + Quantidade.GetHashCode();
			hashCode = hashCode * -1521134295 + VlrTotalItem.GetHashCode();
			return hashCode;
		}
	}
}
