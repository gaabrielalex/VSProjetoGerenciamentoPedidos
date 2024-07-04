using DAOGerenciamentoPedidos;
using DAOGerenciamentoPedidos.Src;
using DAOGerenciamentoPedidos.Src.Data_Base;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.pages.ItemPedidoPages.FormAddEditItemPedido;
using WebGereciamentoPedidos.src.pages.ItemPedidoPages.ListagemItensDoPedido;
using WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos_Novo_;
using WebGereciamentoPedidos.src.util;
using static ModelsGerenciamentoPedidos.Src.StatusPedido;
using static WebGereciamentoPedidos.src.util.MensagemInfo;

namespace WebGereciamentoPedidos.src.pages.PedidoPages.FormAddEditPedido
{
	public partial class FormAddEditPedido : System.Web.UI.UserControl
	{
		private readonly PedidoDAO _pedidoDAO = new PedidoDAO(new BancoDeDados());
		private readonly ItemPedidoDAO _itemPedidoDAO = new ItemPedidoDAO(new BancoDeDados());
		private readonly MetodoPagamentoDAO _metodoPagamentoDAO = new MetodoPagamentoDAO(new BancoDeDados());
		private readonly string _tituloPadraoCadastro = "Cadastrar Pedido";
		private readonly string _tituloPadraoEdicao = "Editar Pedido";
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

		public bool EhModoEdicaoSoQueComAparenciaDeCadastro
		{
			get
			{
				if (ViewState["EhModoEdicaoSoQueComAparenciaDeCadastro"] != null)
				{
					return (bool)ViewState["EhModoEdicaoSoQueComAparenciaDeCadastro"];
				}
				return false;
			}
			set
			{
				ViewState["EhModoEdicaoSoQueComAparenciaDeCadastro"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.QueryString["id"] != null)
				{
					var idPedidoParaEdicao = int.Parse(Request.QueryString["id"]);
					ConfigurarForm(ModosFomularios.Editar, idPedidoParaEdicao);
				}
				else
				{
					ConfigurarForm(ModosFomularios.Cadastrar, null);
				}
			}
		}

		private void ConfigurarForm(ModosFomularios modo, int? idPedidoParaEdicao)
		{
			ModoAtual = modo;
			CarregarConfiguracoesPadraoDoForm(idPedidoParaEdicao);
			if (modo == ModosFomularios.Cadastrar)
				ConfigurarFormParaCadastro();	
			else if (idPedidoParaEdicao.HasValue && modo == ModosFomularios.Editar)
				ConfigurarFormParaEdicao(idPedidoParaEdicao.Value);
		}

		private void CarregarConfiguracoesPadraoDoForm(int? idPedidoParaEdicao)
		{
			if(idPedidoParaEdicao.HasValue)
			{
				FormAddEditItemPedido.ConfigurarForm(ModosFomularios.Cadastrar, idPedidoParaEdicao.Value, null);
			}
			CarregarTodosOsMetodosPagamento();
			CarregarTodosOsStatusDoPedido();
		}

		private void ConfigurarFormParaCadastro()
		{
			FormAddEditPedidoTituloMedio.Text = _tituloPadraoCadastro;
			ClienteTextFormField.Text = string.Empty;
			ObservacoesTextFormField.Text = string.Empty;
			VlrSubtotalTextFormField.Text = "00,00";
			DescontoTextFormField.Text = "00,00";
			VlrTotalTextFormField.Text = "00,00";
			MetodoPagtoDropDownList.ClearSelection();
			StatusDropDownList.ClearSelection();
			DataHoraPedidoDataPicker.ResetarData();
			ListagemItensDoPedido.CarregarItensPedido(new List<ItemPedido>());
		}

		private void ConfigurarFormParaEdicao(int idPedidoParaEdicao)
		{
			try
			{
				PedidoASerEditado = _pedidoDAO.ObterPorId(idPedidoParaEdicao);
				if (PedidoASerEditado == null)
				{
					TratarPedidoNaoEncontrado();
					return;
				}
				PedidoASerEditado.ItensPedido = (List<ItemPedido>)_itemPedidoDAO.ListarPorPedido(idPedidoParaEdicao);

				if (EhModoEdicaoSoQueComAparenciaDeCadastro)
					FormAddEditPedidoTituloMedio.Text = _tituloPadraoCadastro;
				else
					FormAddEditPedidoTituloMedio.Text = _tituloPadraoEdicao;

				ListagemItensDoPedido.CarregarItensPedido(PedidoASerEditado.ItensPedido);
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

		private void CarregarTodosOsMetodosPagamento()
		{
			try
			{
				var listaMetodoPagamento = _metodoPagamentoDAO.ListarTodos();
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

		protected void CancelarPedidoButton_Click(object sender, EventArgs e)
		{
			if(ModoAtual == ModosFomularios.Cadastrar)
				ConfigurarFormParaCadastro();
			else if (ModoAtual == ModosFomularios.Editar)
				ConfigurarFormParaEdicao(PedidoASerEditado.IdPedido.Value);
		}

		protected void SalvarPedidoButton_Click(object sender, EventArgs e)
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
			try
			{
				metodoPagamento = new MetodoPagamento(int.Parse(valueMetodoPAgto), null);
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao obter o método de pagamento", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao obter o método de pagamento: {ex.ToString()}");
				return null;
			}

			DateTime dataHoraPedido;
			try
			{
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
				var idPedidoInserido = _pedidoDAO.Inserir(Pedido);
				PageUtils.MostrarMensagemViaToast("Pedido cadastrado com sucesso", TiposMensagem.Sucesso, Page);

				EhModoEdicaoSoQueComAparenciaDeCadastro = true;
				ConfigurarForm(ModosFomularios.Editar, idPedidoInserido);
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
				_pedidoDAO.Editar(Pedido, PedidoASerEditado.IdPedido.Value);
				PageUtils.MostrarMensagemViaToast("Pedido editado com sucesso", TiposMensagem.Sucesso, Page);
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
			if (dataHoraPedido.Year < DateTime.Now.Year)
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

		private void TratarPedidoNaoEncontrado()
		{
			PageUtils.RedirecionarParaPagina(
				page: Page,
				request: Request,
				urlPagina: "~/src/pages/PedidoPages/Pedidos/Pedidos.aspx",
				MensagemAposRedirecionamento: "Pedido não encontrado!",
				tipoMensagem: TiposMensagem.Erro
			);
		}

		protected void ListagemItensDoPedido_AoEditarItemPedido(int idItemPedido)
		{
			FormAddEditItemPedido.ConfigurarForm(ModosFomularios.Editar, PedidoASerEditado.IdPedido.Value, idItemPedido);
		}

		protected void FormAddEditItemPedido_AposSucessoDoSalvar()
		{
			PedidoASerEditado.ItensPedido = (List<ItemPedido>)_itemPedidoDAO.ListarPorPedido(PedidoASerEditado.IdPedido.Value);
			VlrSubtotalTextFormField.Text = PedidoASerEditado.VlrSubtotal.ToString();
			ListagemItensDoPedido.CarregarItensPedido(PedidoASerEditado.ItensPedido);
		}
	}
}