using DAOGerenciamentoPedidos;
using DAOGerenciamentoPedidos.Src;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.util;
using static ModelsGerenciamentoPedidos.Src.StatusPedido;
using static WebGereciamentoPedidos.src.util.MensagemInfo;

namespace WebGereciamentoPedidos.src.pages.PedidoPages.FormAddEditPedido
{
	public partial class FormAddEditPedido : System.Web.UI.UserControl
	{
		public PedidoDAO PedidoDAO;
		public MetodoPagamentoDAO MetodoPagamentoDAO;
		public ModosFomularios ModoAtual
		{
			get
			{
				if (ViewState["ModoAtual"] != null)
				{
					return (ModosFomularios)ViewState["ModoAtual"];
				}
				return ModosFomularios.Cadastrar;
			}
			set
			{
				ViewState["ModoAtual"] = value;
			}
		}
		public Pedido PedidoASerEditado
		{
			get
			{
				if (ViewState["PedidoASerEditado"] != null)
				{
					return (Pedido)ViewState["PedidoASerEditado"];
				}
				return null;
			}
			set
			{
				ViewState["PedidoASerEditado"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			PedidoDAO = new PedidoDAO();
			MetodoPagamentoDAO = new MetodoPagamentoDAO();

			if (!IsPostBack)
			{
				CarregarTodosOsMetodosPagamento();
				CarregarTodosOsStatusDoPedido();
			}
			
		}

		private void CarregarTodosOsMetodosPagamento()
		{
			try
			{
				List<MetodoPagamento> listaMetodoPagamento = MetodoPagamentoDAO.ListarTodos();
				MetodoPagtoDropDownList.DataSource = listaMetodoPagamento;
				MetodoPagtoDropDownList.DataTextField = "Descricao";
				MetodoPagtoDropDownList.DataValueField = "IdMetodoPagto";
				MetodoPagtoDropDownList.DataBind();
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao carregar os métodos de pagamento", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao carregar métodos de pagamento: {ex.ToString()}");
			}
		}

		private void CarregarTodosOsStatusDoPedido()
		{
			try
			{
				List<StatusPedido> listaStatusPedido = StatusPedido.ObterTodosStatusPedido();
				StatusDropDownList.DataSource = listaStatusPedido;
				StatusDropDownList.DataTextField = "Descricao";
				StatusDropDownList.DataValueField = "Status";
				StatusDropDownList.DataBind();
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao carregar os status do pedido", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao carregar os status do pedido: {ex.ToString()}");
			}
		}

		public void AbrirForm(ModosFomularios modo, int? idPedidoParaEdicao)
		{
			FormAddEditPedidoPanel.Visible = true;
			ModoAtual = modo;
			if (modo == ModosFomularios.Cadastrar)
				ConfigurarFormParaCadastro();
			else if (idPedidoParaEdicao.HasValue && modo == ModosFomularios.Editar)
				ConfigurarFormParaEdicao(idPedidoParaEdicao.Value);

		}

		private void ConfigurarFormParaCadastro()
		{
			FormAddEditPedidoTituloMedio.Text = "Cadastrar Pedido";
			VlrSubtotalTextFormField.Text = "00,00";
			DescontoTextFormField.Text = "00,00";
			VlrTotalTextFormField.Text = "00,00";
		}

		private void ConfigurarFormParaEdicao(int idPedidoParaEdicao)
		{
			try
			{
				FormAddEditPedidoTituloMedio.Text = "Editar Pedido";
				PedidoASerEditado = PedidoDAO.ObterPorId(idPedidoParaEdicao);
				ClienteTextFormField.Text = PedidoASerEditado.NomeCliente;
				MetodoPagtoDropDownList.SelectedValue = PedidoASerEditado.MetodoPagamento.IdMetodoPagto.ToString();
				VlrSubtotalTextFormField.Text = PedidoASerEditado.VlrSubtotal.ToString();
				DescontoTextFormField.Text = PedidoASerEditado.Desconto.ToString();
				VlrTotalTextFormField.Text = PedidoASerEditado.VlrTotal.ToString();
				DataHoraPedidoDataPicker.Date = PedidoASerEditado.DtHrPedido;
				StatusDropDownList.SelectedValue = PedidoASerEditado.StatusPedido.ToString();
				ObservacoesTextFormField.Text = PedidoASerEditado.Observacoes;

			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao obter pedido para edição", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao obter pedido para edição: {ex.ToString()}");
			}
		}

		protected void CancelarButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(Request.RawUrl, true);
		}

		protected void SalvarButton_Click(object sender, EventArgs e)
		{
			if (!Page.IsValid)
			{
				PageUtils.FecharLoadingModal(Page);
				return;
			}

			Pedido Pedido = ObterDadosDoFormulario();
			if (Pedido == null)
				return;

			if (ModoAtual == ModosFomularios.Cadastrar)
			{
				CadastarPedido(Pedido);
			}
			else if (ModoAtual == ModosFomularios.Editar)
			{
				EditarPedido(Pedido);
			}
		}

		private Pedido ObterDadosDoFormulario()
		{
			string nomeCliente = ClienteTextFormField.Text;

			if (!decimal.TryParse(DescontoTextFormField.Text, out decimal desconto))
			{
				PageUtils.MostrarMensagemViaToast("Favor informar valores numéricos no campo \"Desconto\"", TiposMensagem.Erro, Page);
				return null;
			}

			string valueMetodoPAgto = MetodoPagtoDropDownList.SelectedValue;
			MetodoPagamento metodoPagamento;
			try {
				metodoPagamento = new MetodoPagamento(int.Parse(valueMetodoPAgto), null);
			} catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao obter o método de pagamento", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao obter o método de pagamento: {ex.ToString()}");
				return null;
			}

			DateTime dataHoraPedido;
			try {
				dataHoraPedido = DataHoraPedidoDataPicker.Date;
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao obter a data e hora do pedido", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao obter a data e hora do pedido: {ex.ToString()}");
				return null;
			}

			string valueStatusPedido = StatusDropDownList.SelectedValue;
			if (!Enum.TryParse(valueStatusPedido, out EnumStatusPedido statusPedido))
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao obter o status do pedido", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao obter o método de pagamento, erro de conversão");
				return null;
			}
			
			string observacoes = ObservacoesTextFormField.Text;
			
			return new Pedido()
			{
				NomeCliente = nomeCliente,
				Desconto = desconto,
				MetodoPagamento = metodoPagamento,
				DtHrPedido = dataHoraPedido,
				StatusPedido = statusPedido,
				Observacoes = observacoes
			};
		}

		private void CadastarPedido(Pedido Pedido)
		{
			try
			{
				PedidoDAO.Inserir(Pedido);
				//Antes de recarrregar a página, guarda a mensagem de sucesso numa session
				//para que a página principal possa exibi-la depois que for carregada
				Session["MensagemInfo"] = new MensagemInfo { Mensagem = "Pedido cadastrado com sucesso", Tipo = TiposMensagem.Sucesso };
				Response.Redirect(Request.RawUrl, false);
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao cadastrar o pedido", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao cadatsrar pedido: {ex.ToString()}");
			}
		}

		private void EditarPedido(Pedido Pedido)
		{
			try
			{
				PedidoDAO.Editar(Pedido, PedidoASerEditado.IdPedido.Value);
				//Antes de recarrregar a página, guarda a mensagem de sucesso numa session
				//para que a página principal possa exibi-la depois que for carregada
				Session["MensagemInfo"] = new MensagemInfo { Mensagem = "Pedido editado com sucesso", Tipo = TiposMensagem.Sucesso };
				Response.Redirect(Request.RawUrl, false);
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao editar o pedido", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao editar pedido: {ex.ToString()}");
			}
		}

		protected void ClienteTextFormField_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string cliente = args.Value;

			if (string.IsNullOrWhiteSpace(cliente))
			{
				ClienteTextFormField.ErrorMessage = "Campo obrigatório!";
				args.IsValid = false;
				return;
			}

			if (cliente.Length < 3)
			{
				ClienteTextFormField.ErrorMessage = "Tamanho mínimo de 3 caracteres não atingido!";
				args.IsValid = false;
				return;
			}

			if (cliente.Length > 100)
			{
				ClienteTextFormField.ErrorMessage = "Tamanho máximo de 100 caracteres excedido!";
				args.IsValid = false;
				return;
			}
		}

		protected void DescontoTextFormField_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string desconto = args.Value;

			//Validação se campo obrigatório
			if (string.IsNullOrWhiteSpace(desconto))
			{
				DescontoTextFormField.ErrorMessage = "Campo obrigatório!";
				args.IsValid = false;
				return;
			}

			//Validação de valor numérico
			if (!decimal.TryParse(desconto, out decimal descontoDecimal))
			{
				DescontoTextFormField.ErrorMessage = "Valor inválido!";
				args.IsValid = false;
				return;
			}

			//Validação de valor máximo de dígitos
			string digitosString = descontoDecimal.ToString();
			if (!digitosString.Contains(','))
			{
				digitosString += ",0";
			}
			string[] digitos = (digitosString).Split(',');
			if (digitos[0].Length > 6 || digitos[1].Length > 2)
			{
				DescontoTextFormField.ErrorMessage = "Valor deve ter no máximo 8 dígitos, sendo 6 inteiros e 2 decimais!";
				args.IsValid = false;
				return;
			}

			//Validação se o desconto é maior que o valor subtotal
			if (decimal.TryParse(VlrSubtotalTextFormField.Text, out decimal vlrSubtotal))
			{
				if (descontoDecimal > vlrSubtotal)
				{
					DescontoTextFormField.ErrorMessage = "Desconto não pode ser maior que o valor subtotal!";
					args.IsValid = false;
					return;
				}
			}
		}

		protected void DataHoraPedidoDataPicker_ServerValidate(object source, ServerValidateEventArgs args)
		{
			DateTime dataHoraPedido = DataHoraPedidoDataPicker.Date;

			if (dataHoraPedido == DateTime.MinValue)
			{
				DataHoraPedidoDataPicker.ErrorMessage = "Campo obrigatório!";
				args.IsValid = false;
				return;
			}

			if (dataHoraPedido > DateTime.Now)
			{
				DataHoraPedidoDataPicker.ErrorMessage = "Data e hora do pedido não pode ser maior que a data e hora atual!";
				args.IsValid = false;
				return;
			}

			//Data e hora não pode ser anterior ao ano atual
			if(dataHoraPedido.Year < DateTime.Now.Year)
			{
				DataHoraPedidoDataPicker.ErrorMessage = "Data e hora do pedido não pode ser anterior ao ano atual!";
				args.IsValid = false;
				return;
			}
		}

		protected void ObservacoesTextFormField_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string observacoes = args.Value;

			if (observacoes.Length > 0 && observacoes.Length < 10)
			{
				ObservacoesTextFormField.ErrorMessage = "Tamanho mínimo de 10 caracteres não atingido!";
				args.IsValid = false;
				return;
			}

			if (observacoes.Length > 500)
			{
				ObservacoesTextFormField.ErrorMessage = "Tamanho máximo de 500 caracteres excedido!";
				args.IsValid = false;
				return;
			}

		}
	}
}