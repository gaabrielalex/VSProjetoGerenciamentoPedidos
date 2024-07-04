using DAOGerenciamentoPedidos.Src.Data_Base;
using DAOGerenciamentoPedidos.Src;
using DAOGerenciamentoPedidos;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ModelsGerenciamentoPedidos.Src.StatusPedido;
using static WebGereciamentoPedidos.src.util.MensagemInfo;
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.util;

namespace WebGereciamentoPedidos.src.pages.ItemPedidoPages.FormAddEditItemPedido
{
	public partial class FormAddEditItemPedido : System.Web.UI.UserControl
	{
		private readonly ItemPedidoDAO _itemPedidoDAO = new ItemPedidoDAO(new BancoDeDados());
		private int? IdPedidoVinculacao
		{
			get
			{
				if (ViewState["IdPedidoVinculacao"] != null)
				{
					return (int)ViewState["IdPedidoVinculacao"];
				}
				return null;
			}
			set
			{
				ViewState["IdPedidoVinculacao"] = value;
			}
		}
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
		public ItemPedido ItemPedidoASerEditado
		{
			get
			{
				if (ViewState["ItemPedidoASerEditado"] != null)
				{
					return (ItemPedido)ViewState["ItemPedidoASerEditado"];
				}
				return null;
			}
			set
			{
				ViewState["ItemPedidoASerEditado"] = value;
			}
		}
		public bool TemPedidoVinculado
		{
			get
			{
				return IdPedidoVinculacao != null;
			}
		}

		public delegate void EventoSemParametroERetorno();

		public event EventoSemParametroERetorno AposSucessoDoSalvar;

		protected void Page_Load(object sender, EventArgs e)
		{
			
		}

		public void ConfigurarForm(ModosFomularios modo, int idPedidoVinculacao, int? idItemPedidoParaEdicao = null)
		{
			ModoAtual = modo;
			IdPedidoVinculacao = idPedidoVinculacao;
			if (modo == ModosFomularios.Cadastrar)
				ConfigurarFormParaCadastro();
			else if (idItemPedidoParaEdicao.HasValue && modo == ModosFomularios.Editar)
				ConfigurarFormParaEdicao(idItemPedidoParaEdicao.Value);
		}

		private void ConfigurarFormParaCadastro()
		{
			FormAddEditItemPedidoTituloMedio.Text = "Cadastrar Item";
			ProdutoTextFormField.Text = string.Empty;
			ProdutoTextFormField.IdDado = string.Empty;
			QtdeTextFormField.Text = string.Empty;
		}

		private void ConfigurarFormParaEdicao(int idItemPedidoParaEdicao)
		{
			try
			{
				ItemPedidoASerEditado = _itemPedidoDAO.ObterPorId(idItemPedidoParaEdicao);
				if (ItemPedidoASerEditado == null)
				{
					TratarPedidoNaoEncontrado();
					return;
				}
				FormAddEditItemPedidoTituloMedio.Text = "Editar Item";
				ProdutoTextFormField.Text = ItemPedidoASerEditado.Produto.Descricao;
				ProdutoTextFormField.IdDado = ItemPedidoASerEditado.Produto.IdProduto.ToString();
				QtdeTextFormField.Text = ItemPedidoASerEditado.Quantidade.ToString();
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao obter o item do pedido para edição", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao obter item do pedido para edição: {ex.ToString()}");
			}
		}

		protected void CancelarItemPedidoButton_Click(object sender, EventArgs e)
		{
			ConfigurarFormParaCadastro();
		}

		protected void SalvarItemPedidoButtonn_Click(object sender, EventArgs e)
		{
			if (!TemPedidoVinculado)
			{
				PageUtils.MostrarMensagemViaToast("Cadastre primeiramente o pedido para prosseguir com o cadastro dos seus itens!", TiposMensagem.Informacao, Page);
				return;
			}
			if (!Page.IsValid)
			{
				PageUtils.FecharLoadingModal(Page);
				return;
			}

			ItemPedido ItemPedido = ObterDadosDoFormulario();
			if (ItemPedido == null)
				return;

			if (ModoAtual == ModosFomularios.Cadastrar)
			{
				CadastarItemPedido(ItemPedido);
			}
			else if (ModoAtual == ModosFomularios.Editar)
			{
				EditarItemPedido(ItemPedido);
			}
		}

		private ItemPedido ObterDadosDoFormulario()
		{
			if (!int.TryParse(ProdutoTextFormField.IdDado, out int idProduto))
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao salvar os dados do produto vinculado ao item do pedido", TiposMensagem.Erro, Page);
				return null;
			}

			if (!int.TryParse(QtdeTextFormField.Text, out int quantidade))
			{
				PageUtils.MostrarMensagemViaToast("Favor informar valores numéricos no campo \"Quantidade\"", TiposMensagem.Erro, Page);
				return null;
			}

			Produto produto;
			try
			{
				produto = new ProdutoDAO(new BancoDeDados()).ObterPorId(idProduto);
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao salvar os dados do produto vinculado ao item do pedido", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao salvar os dados do produto vinculado ao item do pedido: {ex.ToString()}");
				return null;
			}

			return new ItemPedido(
				idPedido: IdPedidoVinculacao.Value,
				produto: produto,
				quantidade: quantidade
			);
		}

		private void CadastarItemPedido(ItemPedido itemPedido)
		{
			try
			{
				_itemPedidoDAO.Inserir(itemPedido);
				PageUtils.MostrarMensagemViaToast("Item cadastrado com sucesso", TiposMensagem.Sucesso, Page);
				ConfigurarFormParaCadastro();
				AposSucessoDoSalvar?.Invoke();
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao cadastrar o item", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao cadatsrar item do pedido: {ex.ToString()}");
			}
		}

		private void EditarItemPedido(ItemPedido itemPedido)
		{
			try
			{
				_itemPedidoDAO.Editar(itemPedido, ItemPedidoASerEditado.IdItemPedido.Value);
				PageUtils.MostrarMensagemViaToast("Item editado com sucesso", TiposMensagem.Sucesso, Page);
				ConfigurarFormParaCadastro();
				AposSucessoDoSalvar?.Invoke();
			}
			catch (Exception ex)
			{
				PageUtils.MostrarMensagemViaToast("Houve um erro ao editar o item", TiposMensagem.Erro, Page);
				RegistroLog.Log($"Erro ao editar o item do pedido: {ex.ToString()}");
			}
		}

		private void TratarPedidoNaoEncontrado()
		{
			PageUtils.MostrarMensagemViaToastComDelay("Item não encontrado", TiposMensagem.Erro, Page);
			AdicionarScriptParaRedirecionamentoComDelay();
		}

		private void AdicionarScriptParaRedirecionamentoComDelay()
		{
			string previousPageUrl = Request.UrlReferrer?.ToString();
			if (string.IsNullOrEmpty(previousPageUrl))
			{
				previousPageUrl = ResolveUrl("~/src/pages/PedidoPages/Pedidos/Pedidos.aspx");
			}

			string script = $@"
					setTimeout(function() {{
						window.location.href = '{previousPageUrl}';
					}}, 1500);";

			ScriptManager.RegisterStartupScript(this, this.GetType(), "redirecionarComDelay", script, true);
		}

		protected void QtdeTextFormField_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string quantidadeString = args.Value;

			if (!TemPedidoVinculado)
			{
				args.IsValid = false;
				return;
			}

			if (string.IsNullOrWhiteSpace(quantidadeString))
			{
				QtdeTextFormField.ErrorMessage = "Campo obrigatório!";
				args.IsValid = false;
				return;
			}

			if (!int.TryParse(quantidadeString, out int quantidade))
			{
				QtdeTextFormField.ErrorMessage = "Favor informar um valor numérico!";
				args.IsValid = false;
				return;
			}
			
			if (quantidade <= 0)
			{
				QtdeTextFormField.ErrorMessage = "Favor informar um valor maior que zero!";
				args.IsValid = false;
				return;
			}

			if (quantidade >= 1000000)
			{
				QtdeTextFormField.ErrorMessage = "Favor informar um valor menor que 1.000.000!";
				args.IsValid = false;
				return;
			}
		}

		protected void ProdutoTextFormField_ServerValidate(object source, ServerValidateEventArgs args)
		{
			string idProdutoString = ProdutoTextFormField.IdDado;

			if (!TemPedidoVinculado)
			{
				args.IsValid = false;
				return;
			}

			if (string.IsNullOrWhiteSpace(idProdutoString))
			{
				ProdutoTextFormField.ErrorMessage = "Campo obrigatório!";
				args.IsValid = false;
				return;
			}

			if (!int.TryParse(idProdutoString, out int idProduto))
			{
				ProdutoTextFormField.ErrorMessage = "Houve um erro ao obter o produto!";
				args.IsValid = false;
				return;
			}

			Produto produto;
			try
			{
				produto = new ProdutoDAO(new BancoDeDados()).ObterPorId(idProduto);
			}
			catch (Exception ex)
			{
				ProdutoTextFormField.ErrorMessage = "Houve um erro ao obter o produto!";
				RegistroLog.Log($"Erro ao obter produto: {ex.ToString()}");
				args.IsValid = false;
				return;
			}

			if (produto == null)
			{
				ProdutoTextFormField.ErrorMessage = "Produto não encontrado!";
				args.IsValid = false;
				return;
			}

		}

	}
}