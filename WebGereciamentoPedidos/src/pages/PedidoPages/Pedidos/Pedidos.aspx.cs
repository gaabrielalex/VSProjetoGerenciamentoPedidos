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
				//TODO: Implementar a lógica de exibição de mensagens
				//if (Session["MensagemInfo"] != null)
				//{
				//	//Verifica se alguma outra tela registrou alguma mensagem para ser exibida
				//	MensagemInfo mensagemInfo = (MensagemInfo)Session["MensagemInfo"];
				//	PageUtils.MostrarMensagemViaToastComDelay(mensagemInfo.Mensagem, mensagemInfo.Tipo, this);
				//	//Depois de exibir remove para não haver repetição
				//	Session.Remove("MensagemInfo");
				//}

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

		//TODO: Implementar a lógica de exclusão e edição de pedidos
		protected void PedidosGW_RowCommand(object sender, GridViewCommandEventArgs e)
		{
		//	int idPedidoSelecionado = Convert.ToInt32(e.CommandArgument);
		//	if (idPedidoSelecionado < 0)
		//	{
		//		string menssagem = "Houve um erro ao selecionar o pedido para ação selecionada, entre em contato com suporte caso o erro persista!";
		//		PageUtils.MostrarMensagemViaToast(menssagem, TiposMensagem.Erro, this);
		//		return;
		//	}
		//	else
		//	{
		//		Produto produtoSelecionado = new Produto();
		//		foreach (Produto produto in DadosPedidosAtual)
		//		{
		//			if (produto.IdProduto == idPedidoSelecionado)
		//			{
		//				produtoSelecionado = produto;
		//				break;
		//			}
		//		}

		//		if (e.CommandName == "Excluir")
		//		{
		//			var ehParaExcluir = PageUtils.SolicitarConfirmacaoViaAPISistemaOperacionalLocal("Deseja realmente excluir o produto?");
		//			if (!ehParaExcluir)
		//				return;
		//			try
		//			{
		//				PedidoDAO.Excluir(idPedidoSelecionado);
		//				string mensagem = "Registro excluído com sucesso";
		//				TratarCarregamentoDeDados();
		//				PageUtils.MostrarMensagemViaToast(mensagem, TiposMensagem.Sucesso, this);
		//			}
		//			catch (Exception ex)
		//			{
		//				RegistroLog.Log($"Erro ao excluir produto '{idPedidoSelecionado}' - '{produtoSelecionado.Descricao}' : '{ex.Message}");
		//				string mensagem = "Erro ao deletar o produto";
		//				PageUtils.MostrarMensagemViaToast(mensagem, TiposMensagem.Erro, this);
		//			}

		//		}
		//		else if (e.CommandName == "Editar")
		//		{
		//			ListsagemProdutoPanel.Visible = false;
		//			FormAddEditProduto.AbrirForm(ModosFomularios.Editar, idPedidoSelecionado);
		//		}
		//	}

		}

		//TODO: Implementar a lógica de criação de novos pedidos
		//protected void NovoPedidoButton_Click(object sender, EventArgs e)
		//{
		//	ListsagemPedidoPanel.Visible = false;
		//	FormAddEditProduto.AbrirForm(ModosFomularios.Cadastrar, null);
		//}


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