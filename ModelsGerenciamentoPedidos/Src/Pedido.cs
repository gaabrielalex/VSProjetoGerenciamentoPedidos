using ModelsGerenciamentoPedidos.Src.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ModelsGerenciamentoPedidos.Src.StatusPedido;

namespace ModelsGerenciamentoPedidos.Src
{
	[Serializable]
	public class Pedido
	{
		public int? IdPedido{  get; set; }
		public List<ItemPedido> ItensPedido{ get; set; }
		public string NomeCliente{ get; set; }
		public decimal VlrSubtotal{ get; set; }
		public decimal Desconto{ get; set; }
		public decimal VlrTotal => VlrSubtotal - Desconto;
		public DateTime DtHrPedido { get; set; }
		public EnumStatusPedido StatusPedido { get; set; }
		public string DescricaoStatusPedido
		{
			get 
			{
				return ModelsGerenciamentoPedidos.Src.StatusPedido.ObterDescricaoStatusPedido(StatusPedido);
			}
		}
		public string Observacoes{ get; set; }
		public MetodoPagamento MetodoPagamento { get; set; }
		public Pedido(){ }

		//Itens do pedido não são obrigatórios, podendo ser inseridos posteriormente, para que, em casos
		//da necessidade apenas das outras informações, seja possível instanciar um pedido sem os itens
		//evitando o uso desnecessário de memória e processamento
		public Pedido(
			int? idPedido, string nomeCliente, 
			decimal vlrSubtotal, decimal desconto, DateTime dtHrPedido, EnumStatusPedido statusPedido, 
			string observacoes, MetodoPagamento metodoPagamento, List<ItemPedido> itensPedido = null
		)
		{
			IdPedido = idPedido;
			NomeCliente = nomeCliente;
			VlrSubtotal = vlrSubtotal;
			Desconto = desconto;
			DtHrPedido = dtHrPedido;
			StatusPedido = statusPedido;
			Observacoes = observacoes;
			MetodoPagamento = metodoPagamento;
			ItensPedido = itensPedido;
		}

		public void RecalcularVlrTotal()
		{
			VlrSubtotal = ItensPedido.Sum(i => i.VlrTotalItem);
		}

		public override bool Equals(object obj)
		{
			return obj is Pedido pedido &&
				   IdPedido == pedido.IdPedido &&
				   EqualityComparer<List<ItemPedido>>.Default.Equals(ItensPedido, pedido.ItensPedido) &&
				   NomeCliente == pedido.NomeCliente &&
				   VlrSubtotal == pedido.VlrSubtotal &&
				   Desconto == pedido.Desconto &&
				   VlrTotal == pedido.VlrTotal &&
				   DtHrPedido == pedido.DtHrPedido &&
				   StatusPedido == pedido.StatusPedido &&
				   DescricaoStatusPedido == pedido.DescricaoStatusPedido &&
				   Observacoes == pedido.Observacoes &&
				   EqualityComparer<MetodoPagamento>.Default.Equals(MetodoPagamento, pedido.MetodoPagamento);
		}

		public override int GetHashCode()
		{
			int hashCode = -7071731;
			hashCode = hashCode * -1521134295 + IdPedido.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<List<ItemPedido>>.Default.GetHashCode(ItensPedido);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NomeCliente);
			hashCode = hashCode * -1521134295 + VlrSubtotal.GetHashCode();
			hashCode = hashCode * -1521134295 + Desconto.GetHashCode();
			hashCode = hashCode * -1521134295 + VlrTotal.GetHashCode();
			hashCode = hashCode * -1521134295 + DtHrPedido.GetHashCode();
			hashCode = hashCode * -1521134295 + StatusPedido.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DescricaoStatusPedido);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Observacoes);
			hashCode = hashCode * -1521134295 + EqualityComparer<MetodoPagamento>.Default.GetHashCode(MetodoPagamento);
			return hashCode;
		}
	}
}