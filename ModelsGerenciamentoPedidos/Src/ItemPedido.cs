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
	}
}
