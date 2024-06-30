using DAOGerenciamentoPedidos.Src.Data_Base;
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
using WebGereciamentoPedidos.src.util;

namespace WebGereciamentoPedidos.src.pages.ItemPedidoPages.ListagemItensDoPedido
{
	public partial class ListagemItensDoPedido : System.Web.UI.UserControl
	{
		private readonly ItemPedidoDAO _itemPedidoDAO = new ItemPedidoDAO(new BancoDeDados());

		private IList<ItemPedido> DadosItensPedidoAtual
		{
			get
			{
				if (ViewState["DadosItensPedidoAtual"] != null)
				{
					return (IList<ItemPedido>)ViewState["DadosItensPedidoAtual"];
				}
				return new List<ItemPedido>();
			}
			set
			{
				ViewState["DadosItensPedidoAtual"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				TratarCarregamentoDeDados();	
			}
		}

		public void CarregarItensPedido(IList<ItemPedido> FonteDeDados)
		{
			DadosItensPedidoAtual = FonteDeDados;
			TratarCarregamentoDeDados();
		}

		//TODO: É realmente necessário o mostrar msg via toast com delay ou apenas sem o delay já está suficiente?
		private void TratarCarregamentoDeDados()
		{
			try
			{
				BindData();
			}
			catch (Exception ex)
			{
				RegistroLog.Log($"Erro ao listar itens do pedido: {ex.ToString()}");
				PageUtils.MostrarMensagemViaToastComDelay("Erro ao listar itens do pedido", TiposMensagem.Erro, Page);
			}
		}
		private void BindData()
		{
			ItensPedidosGW.DataSource = DadosItensPedidoAtual;
			ItensPedidosGW.DataBind();
		}

		protected void ItensPedidosGW_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int idItemPedidoSelecionado = Convert.ToInt32(e.CommandArgument);
			if (idItemPedidoSelecionado < 0)
			{
				string menssagem = "Houve um erro ao selecionar o item do pedido para ação selecionada, entre em contato com suporte caso o erro persista!";
				PageUtils.MostrarMensagemViaToast(menssagem, TiposMensagem.Erro, Page);
				return;
			}
			else
			{
				//TODO: Implementar essa parte
				if (e.CommandName == "Editar")
				{
					//FormAddEditProduto.AbrirForm(ModosFomularios.Editar, idProdutoSelecionado);
				}
			}
		}
	}
}