using DAOGerenciamentoPedidos.Src.Data_Base;
using DAOGerenciamentoPedidos;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static WebGereciamentoPedidos.src.util.MensagemInfo;
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.util;

namespace WebGereciamentoPedidos.src.pages.PedidoPages.ListagemPedidos
{
	public partial class ListagemPedidos : System.Web.UI.UserControl
	{
		private readonly PedidoDAO _pedidoDAO = new PedidoDAO(new BancoDeDados());
		public IList<Pedido> DadosPedidosAtual
		{
			get
			{
				if (ViewState["DadosPedidoAtual"] != null)
				{
					return (IList<Pedido>)ViewState["DadosPedidoAtual"];
				}
				return new List<Pedido>();
			}
			set
			{
				ViewState["DadosPedidoAtual"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{
				TratarCarregamentoDeDados();
			}
		}

		private void TratarCarregamentoDeDados()
		{
			try
			{
				if (Request.QueryString["Filtro"] != null)
				{
					string filtro = Request.QueryString["Filtro"];

					DadosPedidosAtual = _pedidoDAO.ListarPorCliente(filtro);
				}
				else
				{
					DadosPedidosAtual = _pedidoDAO.ListarTodos();
				}

				BindData();
			}
			catch (Exception ex)
			{
				RegistroLog.Log($"Erro ao listar pedidos: {ex.ToString()}");
				PageUtils.MostrarMensagemViaToastComDelay("Erro ao listar pedidos", TiposMensagem.Erro, Page);
			}
		}
		private void BindData()
		{
			PedidosGW.DataSource = DadosPedidosAtual;
			PedidosGW.DataBind();
		}

		protected void PedidoFiltro_FiltrarClick(object sender, EventArgs e)
		{
			var filtro = PedidoFiltro.Text.Trim();
			//Obtem apenas a url sem possíveis query parameters no meio, desta
			//forma, ousuário não faz ter o mesmo query parametres na url
			var urlAtual = Request.Url.GetLeftPart(UriPartial.Path);
			String novaUrl;
			if (filtro == "" || filtro == null)
			{
				novaUrl = urlAtual;
			}
			else
			{
				novaUrl = urlAtual + $"?filtro={filtro}";
			}

			Response.Redirect(novaUrl, false);
		}

		protected void PedidosGW_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int idPedidoSelecionado = Convert.ToInt32(e.CommandArgument);
			if (idPedidoSelecionado < 0)
			{
				string menssagem = "Houve um erro ao selecionar o pedido para ação selecionada, entre em contato com suporte caso o erro persista!";
				PageUtils.MostrarMensagemViaToast(menssagem, TiposMensagem.Erro, Page);
				return;
			}
			else
			{
				if (e.CommandName == "Editar")
				{
					RedirecionarParaPaginaDeEdicaoDoPedido(idPedidoSelecionado);
				}
			}
		}

		protected void NovoPedidoButton_Click(object sender, EventArgs e)
		{
			RedirecionarParaPaginaDeCadastroDoPedido();
		}

		private void RedirecionarParaPaginaDeCadastroDoPedido()
		{
			Response.Redirect("/src/pages/PedidoPages/PedidoPage/PedidoPage.aspx", false);
		}

		private void RedirecionarParaPaginaDeEdicaoDoPedido(int idPedido)
		{
			Response.Redirect($"/src/pages/PedidoPages/PedidoPage/PedidoPage.aspx?id={idPedido}", false);
		}
	}
}