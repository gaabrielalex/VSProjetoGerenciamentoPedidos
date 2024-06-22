using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsGerenciamentoPedidos.Src
{
	public class StatusPedido
	{
		public enum EnumStatusPedido
		{
			AguardandoPagamento = 'A',
			EmSeparacao = 'S',
			Entregue = 'E',
			Cancelado = 'C',
		}

		public EnumStatusPedido Status { get; set; }
		public string Descricao { get; set; }

		public StatusPedido() { }
		
		public static string ObterDescricaoStatusPedido(EnumStatusPedido statusPedido)
		{
			switch (statusPedido)
			{
				case EnumStatusPedido.AguardandoPagamento:
					return "Aguardando Pagamento";
				case EnumStatusPedido.EmSeparacao:
					return "Em Separação";
				case EnumStatusPedido.Entregue:
					return "Entregue";
				case EnumStatusPedido.Cancelado:
					return "Cancelado";
				default:
					return "Status Desconhecido";
			}
		}

		public static List<StatusPedido> ObterTodosStatusPedido()
		{
			List<StatusPedido> listaStatusPedidos = new List<StatusPedido>();
			Enum.GetValues(typeof(EnumStatusPedido)).Cast<EnumStatusPedido>().ToList().ForEach(
				status => {
					listaStatusPedidos.Add(new StatusPedido() { Status = status, Descricao = ObterDescricaoStatusPedido(status) });
				}
			);

			return listaStatusPedidos;
		}

		//public static List<StatusPedido> ObterTodosStatusPedido()
		//{
		//	Enum.GetValues(typeof(EnumStatusPedido)).Cast<EnumStatusPedido>().ToList().ForEach(status => ObterDescricaoStatusPedido(status)); //TODO:

		//	return
		//	List < StatusPedido > statusPedidos = new List<StatusPedido>();
		//	statusPedidos.Add(new StatusPedido() { Status = EnumStatusPedido.AguardandoPagamento, Descricao = ObterDescricaoStatusPedido(EnumStatusPedido.AguardandoPagamento) });
		//	statusPedidos.Add(new StatusPedido() { Status = EnumStatusPedido.EmSeparacao, Descricao = ObterDescricaoStatusPedido(EnumStatusPedido.EmSeparacao) });
		//	statusPedidos.Add(new StatusPedido() { Status = EnumStatusPedido.Entregue, Descricao = ObterDescricaoStatusPedido(EnumStatusPedido.Entregue) });
		//	statusPedidos.Add(new StatusPedido() { Status = EnumStatusPedido.Cancelado, Descricao = ObterDescricaoStatusPedido(EnumStatusPedido.Cancelado) });
		//	return statusPedidos;
		//}
	}
}
