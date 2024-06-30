﻿using DAOGerenciamentoPedidos.Src;
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
using DAOGerenciamentoPedidos.Src.Data_Base;

namespace WebGereciamentoPedidos.src.pages.PedidoPages.Pedidos
{
	public partial class Pedidos : System.Web.UI.Page
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
				PageUtils.MostrarMensagemViaToastComDelay("Erro ao listar pedidos", TiposMensagem.Erro, this);
			}
		}
		private void BindData()
		{
			PedidosGW.DataSource = DadosPedidosAtual;
			PedidosGW.DataBind();
		}

		protected void PedidoFiltro_FiltrarClick(object sender, EventArgs e)
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
				if (e.CommandName == "Editar")
				{
					RedirecionarParaEditarPedido(idPedidoSelecionado);
				}
			}
		}

		protected void NovoPedidoButton_Click(object sender, EventArgs e)
		{
			RedirecionarParaCadastrarPedido();
		}

		private void RedirecionarParaCadastrarPedido()
		{
			Response.Redirect("/src/pages/PedidoPages/PedidoPage/PedidoPage.aspx", false);
		}

		private void RedirecionarParaEditarPedido(int idPedido)
		{
			Response.Redirect($"/src/pages/PedidoPages/PedidoPage/PedidoPage.aspx?id={idPedido}", false);
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static string ExcluirPedido(int id)
		{
			new PedidoDAO(new BancoDeDados()).Excluir(id);

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