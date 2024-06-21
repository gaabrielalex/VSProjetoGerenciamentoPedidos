using DAOGerenciamentoPedidos.Src;
using ModelsGerenciamentoPedidos.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static WebGereciamentoPedidos.src.util.MensagemInfo;
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.pages.ProdutoPages.FormAddEditProduto;
using WebGereciamentoPedidos.src.util;
using DAOGerenciamentoPedidos;

namespace WebGereciamentoPedidos.src.pages.PedidoPages.Pedidos
{
	public partial class Pedidos : System.Web.UI.Page
	{
		public PedidoDAO PedidoDAO;
		public List<Pedido> DadosPedidosAtual
		{
			get
			{
				if (ViewState["DadosPedidoAtual"] != null)
				{
					return (List<Pedido>)ViewState["DadosPedidoAtual"];
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
			//Inicializando atributos
			PedidoDAO = new PedidoDAO();

			if (!IsPostBack)
			{

				if (Session["MensagemInfo"] != null)
				{
					//Verifica se alguma outra tela registrou alguma mensagem para ser exibida
					MensagemInfo mensagemInfo = (MensagemInfo)Session["MensagemInfo"];
					PageUtils.MostrarMensagemViaToastComDelay(mensagemInfo.Mensagem, mensagemInfo.Tipo, this);
					//Depois de exibir remove para não haver repetição
					Session.Remove("MensagemInfo");
				}

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

					DadosPedidosAtual = PedidoDAO.ListarPorCliente(filtro);
				}
				else
				{
					DadosPedidosAtual = PedidoDAO.Listar();
				}

				BindData();
			}
			catch (Exception ex)
			{
				RegistroLog.Log($"Erro ao listar pedidos: {ex.ToString()}");
				PageUtils.MostrarMensagemViaToastComDelay("Erro ao listar pedidos", TiposMensagem.Erro, this);
			}
		}
		private void BindData()
		{
			PedidosGW.DataSource = DadosPedidosAtual;
			PedidosGW.DataBind();
		}

		protected void ProdutoFiltro_FiltrarClick(object sender, EventArgs e)
		{
			String filtro = PedidoFiltro.Text;
			//Obtem apenas a url sem possíveis query parameters no meio, desta
			//forma, ousuário não faz ter o mesmo query parametres na url
			String urlAtual = Request.Url.GetLeftPart(UriPartial.Path);
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
				PageUtils.MostrarMensagemViaToast(menssagem, TiposMensagem.Erro, this);
				return;
			}
			else
			{
				Pedido pedidoSelecionado = new Pedido();
				foreach (Pedido pedido in DadosPedidosAtual)
				{
					if (pedido.IdPedido == idPedidoSelecionado)
					{
						pedidoSelecionado = pedido;
						break;
					}
				}

				if (e.CommandName == "Editar")
				{
					ListsagemPedidoPanel.Visible = false;
					FormAddEditPedido.AbrirForm(ModosFomularios.Editar, idPedidoSelecionado);
				}
			}

		}

		protected void NovoPedidoButton_Click(object sender, EventArgs e)
		{
			ListsagemPedidoPanel.Visible = false;
			FormAddEditPedido.AbrirForm(ModosFomularios.Cadastrar, null);
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static string ExcluirPedido(int id)
		{
			new PedidoDAO().Excluir(id);

			var response = new
			{
				Message = $"Registro {id} excluído com sucesso",
				Success = true
			};

			// Serializa o objeto para JSON
			JavaScriptSerializer js = new JavaScriptSerializer();
			return js.Serialize(response);
		}

	}
}