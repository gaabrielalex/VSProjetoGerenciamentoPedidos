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
		

		private const int MaxLengthNomeCliente = 100;
		private const decimal MinVlrTotal = 0;
		private const int MaxDigitosParteInteiraVlrTotal = 7;
		private const decimal MinDesconto = 0;
		private const int MaxDigitosParteInteiraDesconto = 6;
		private const int MaxLengthObservacoes = 200;
		private string _nomeCliente;
		private decimal _vlrSubtotal;
		private decimal _desconto;
		private string _observacoes;

		public int? IdPedido{  get; set; }
		public string NomeCliente 
		{
			get { return _nomeCliente; }
			set 
			{ 
				if(value.Length > MaxLengthNomeCliente)
				{
					throw new ArgumentOutOfRangeException(nameof(value), $"O valor deve uma tamnho máximo de {MaxLengthNomeCliente} caracteres.");
				}
				_nomeCliente = value;
			}
		}
		public decimal VlrSubtotal 
		{
			get { return _vlrSubtotal; }
			set 
			{
				if(value < MinVlrTotal || MaxDigitosParteInteiraVlrTotal < ModelUtils.ObterQuantidadeDeDigitosAntesDoSeparadorDecimal(value))
				{
					throw new ArgumentOutOfRangeException(nameof(value), $"O valor deve ser maior ou igual a {MinVlrTotal} e ter no máximo {MaxDigitosParteInteiraVlrTotal} dígitos na sua parte inteira.");
				}
				_vlrSubtotal = value;
			}	
		}
		public decimal Desconto
		{
			get { return _desconto; }
			set
			{
				if (value < MinDesconto || MaxDigitosParteInteiraDesconto < ModelUtils.ObterQuantidadeDeDigitosAntesDoSeparadorDecimal(value))
				{
					throw new ArgumentOutOfRangeException(nameof(value), $"ser maior ou igual a  {MinDesconto}  e ter no máximo  {MaxDigitosParteInteiraVlrTotal}  dígitos na sua parte inteira.");
				}
				_desconto = value;
			}
		}

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
		public string Observacoes
		{
			get { return _observacoes; }
			set
			{
				if (value.Length > MaxLengthObservacoes)
				{
					throw new ArgumentOutOfRangeException(nameof(value), $"O valor deve uma tamnho máximo de {MaxLengthObservacoes} caracteres.");
				}
				_observacoes = value;
			}
		}
		public MetodoPagamento MetodoPagamento { get; set; }

		public Pedido(){ }

		public Pedido(int? idPedido, string nomeCliente, decimal vlrSubtotal, decimal desconto, DateTime dtHrPedido, EnumStatusPedido statusPedido, string observacoes, MetodoPagamento metodoPagemento)
		{
			IdPedido = idPedido;
			NomeCliente = nomeCliente;
			VlrSubtotal = vlrSubtotal;
			Desconto = desconto;
			DtHrPedido = dtHrPedido;
			StatusPedido = statusPedido;
			Observacoes = observacoes;
			MetodoPagamento = metodoPagemento;
		}

		public override bool Equals(object obj)
		{
			return obj is Pedido pedido &&
				   IdPedido == pedido.IdPedido &&
				   NomeCliente == pedido.NomeCliente &&
				   VlrSubtotal == pedido.VlrSubtotal &&
				   Desconto == pedido.Desconto &&
				   DtHrPedido == pedido.DtHrPedido &&
				   StatusPedido == pedido.StatusPedido &&
				   Observacoes == pedido.Observacoes &&
				   EqualityComparer<MetodoPagamento>.Default.Equals(MetodoPagamento, pedido.MetodoPagamento);
		}

		public override int GetHashCode()
		{
			int hashCode = -1499515728;
			hashCode = hashCode * -1521134295 + IdPedido.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NomeCliente);
			hashCode = hashCode * -1521134295 + VlrSubtotal.GetHashCode();
			hashCode = hashCode * -1521134295 + Desconto.GetHashCode();
			hashCode = hashCode * -1521134295 + DtHrPedido.GetHashCode();
			hashCode = hashCode * -1521134295 + StatusPedido.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Observacoes);
			hashCode = hashCode * -1521134295 + EqualityComparer<MetodoPagamento>.Default.GetHashCode(MetodoPagamento);
			return hashCode;
		}
	}
}