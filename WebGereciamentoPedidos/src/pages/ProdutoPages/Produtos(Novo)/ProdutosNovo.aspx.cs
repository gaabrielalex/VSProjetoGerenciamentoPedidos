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
using UtilsGerenciamentoPedidos;
using WebGereciamentoPedidos.src.util;
using static WebGereciamentoPedidos.src.util.MensagemInfo;

namespace WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos_Novo_
{
	public partial class ProdutosNovo : System.Web.UI.Page
	{
		public ProdutoDAO ProdutoDAO;
		public List<Produto> DadosProdutosAtual
		{
			get
			{
				if (ViewState["DadosProdutosAtual"] != null)
				{
					return (List<Produto>)ViewState["DadosProdutosAtual"];
				}
				return new List<Produto>();
			}
			set
			{
				ViewState["DadosProdutosAtual"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			//Inicializando atributos
			ProdutoDAO = new ProdutoDAO();

			if (!IsPostBack)
			{	
				if(Session["MensagemInfo"] != null) 
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
			if (Request.QueryString["Filtro"] != null)
			{
				string filtro = Request.QueryString["Filtro"];

				DadosProdutosAtual = ProdutoDAO.ListarPorDescricao(filtro);
			}
			else
			{
				DadosProdutosAtual = ProdutoDAO.Listar();
			}
			BindData();
		}
		private void BindData()
		{
			ProdutosGW.DataSource = DadosProdutosAtual;
			ProdutosGW.DataBind();
		}

		protected void ProdutoFiltro_FiltrarClick(object sender, EventArgs e)
		{
			String filtro = ProdutoFiltro.Text;
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

		protected void ProdutosGW_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int idProdutoSelecionado = Convert.ToInt32(e.CommandArgument);
			if (idProdutoSelecionado < 0)
			{
				string menssagem = "Houve um erro ao selecionar o produto para ação selecionada, entre em contato com suporte caso o erro persista!";
				PageUtils.MostrarMensagemViaToast(menssagem, TiposMensagem.Erro, this);
				return;
			}
			else
			{
				Produto produtoSelecionado = new Produto();
				foreach (Produto produto in DadosProdutosAtual)
				{
					if (produto.IdProduto == idProdutoSelecionado)
					{
						produtoSelecionado = produto;
						break;
					}
				}

				if (e.CommandName == "Excluir")
				{
					var ehParaExcluir = PageUtils.SolicitarConfirmacaoViaAPISistemaOperacionalLocal("Deseja realmente excluir o produto?");
					if (!ehParaExcluir)
						return;
					try
					{
						ProdutoDAO.Excluir(idProdutoSelecionado);
						string mensagem = "Registro excluído com sucesso";
						TratarCarregamentoDeDados();
						PageUtils.MostrarMensagemViaToast(mensagem, TiposMensagem.Sucesso, this);
					}
					catch (Exception ex)
					{
						RegistroLog.Log($"Erro ao excluir produto '{ idProdutoSelecionado }' - '{ produtoSelecionado.Descricao }' : '{ex.Message}");
						string mensagem = "Erro ao deletar o produto";
						PageUtils.MostrarMensagemViaToast(mensagem, TiposMensagem.Erro, this);
					}

				}
				else if (e.CommandName == "Editar")
				{
					ListsagemProdutoPanel.Visible = false;
					FormAddEditProduto.AbrirForm(ModosFomularios.Editar, idProdutoSelecionado);
				}
			}

		}

		protected void NovoProdutoButton_Click(object sender, EventArgs e)
		{
			ListsagemProdutoPanel.Visible = false;
			FormAddEditProduto.AbrirForm(ModosFomularios.Cadastrar, null);
		}

		/* Formas antigas que eu estava tentando usar para excluir um projeto colocando um 
		 * painel de confirmação antes de excluir o produto */

		//Teste que eu estava fazendo sem usar ajax
		//protected void ExcluirProduto(object sender, EventArgs e)
		//{
		//	string idProduto = Request.Form["__EVENTARGUMENT"];
		//	int id = int.Parse(idProduto);
		//	ClientScript.RegisterClientScriptBlock(typeof(Page), "Alerta", "<script>alert('Produto excluído com sucesso!');</script>");
		//	ProdutoFiltro.Text = "dvfgwsfrgwer";
		//	DadosProdutosAtual = new List<Produto>();
		//	BindData();
		//	TituloPanelLabel.Text = "Text";
		//	ProdutoDAO.excluir(id);
		//	TratarCarregamentoDeDados();
		//}

		//Excluindo produto com ajax
		//[WebMethod]
		//[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		//public static string ExcluirProduto(int id)
		//{
		//	var houveSucesso = new ProdutoDAO().excluir(id);
		//	if (!houveSucesso)
		//		throw new Exception("Erro ao deletar o produto");

		//	var response = new
		//	{
		//		Message = $"Registro {id} excluído com sucesso",
		//		Success = true
		//	};

		//	// Serializa o objeto para JSON
		//	JavaScriptSerializer js = new JavaScriptSerializer();
		//	return js.Serialize(response);
		//}

		//protected void DepoisDeExcluirProduto(object sender, EventArgs e)
		//{
		//	TratarCarregamentoDeDados();
		//}

	}
}