using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsGerenciamentoPedidos.Src
{
	[Serializable]
	public  class MetodoPagamento
	{
		public int IdMetodoPagto { get; set; }
		public string Descricao { get; set; }

		public MetodoPagamento()
		{
		}

		public MetodoPagamento(int idMetodoPagto, string descricao)
		{
			IdMetodoPagto = idMetodoPagto;
			Descricao = descricao;
		}

		public override bool Equals(object obj)
		{
			return obj is MetodoPagamento pagamento &&
				   IdMetodoPagto == pagamento.IdMetodoPagto &&
				   Descricao == pagamento.Descricao;
		}

		public override int GetHashCode()
		{
			int hashCode = 1308185697;
			hashCode = hashCode * -1521134295 + IdMetodoPagto.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Descricao);
			return hashCode;
		}
	}
}
