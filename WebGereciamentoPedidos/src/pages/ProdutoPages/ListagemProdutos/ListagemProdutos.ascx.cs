using DAOGerenciamentoPedidos.Src.Data_Base;
using DAOGerenciamentoPedidos.Src;
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
using WebGereciamentoPedidos.src.components.ColunasPadraoTable;

namespace WebGereciamentoPedidos.src.pages.ProdutoPages.ListagemProdutos
{
	public partial class ListagemProdutos : System.Web.UI.UserControl
	{
		private readonly ProdutoDAO _produtoDAO = new ProdutoDAO(new BancoDeDados());
		public IList<Produto> DadosProdutosAtual
		{
			get
			{
				if (ViewState["DadosProdutosAtual"] != null)
				{
					return (IList<Produto>)ViewState["DadosProdutosAtual"];
				}
				return new List<Produto>();
			}
			set
			{
				ViewState["DadosProdutosAtual"] = value;
			}
		}
		public bool EhPesquiseLookUp
		{
			get
			{
				if (ViewState["EhPesquiseLookUp"] != null)
				{
					return (bool)ViewState["EhPesquiseLookUp"];
				}
				return false;
			}
			set
			{
				ViewState["EhPesquiseLookUp"] = value;
			}
		}

		public bool PesquisaLookUpDetalhada
		{
			get
			{
				if (ViewState["PesquisaLookUpDetalhada"] != null)
				{
					return (bool)ViewState["PesquisaLookUpDetalhada"];
				}
				return false;
			}
			set
			{
				ViewState["PesquisaLookUpDetalhada"] = value;
			}
		}

		public string ScriptAoSelecionarProduto
		{
			get
			{
				if (ViewState["ScriptAoSelecionarProduto"] != null)
				{
					return (string)ViewState["ScriptAoSelecionarProduto"];
				}
				return string.Empty;
			}
			set
			{
				ViewState["ScriptAoSelecionarProduto"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				if (EhPesquiseLookUp)
				{
					ConfigurarParaPesquisaLookUp();
					TratarCarregamentoDeDadosViaAtualizacaoDiretaDaFonteDeDados(null);
				}
				else
				{
					TratarCarregamentoDeDadosViaQueryParametrer();
				}
			}
		}

		private void TratarCarregamentoDeDadosViaQueryParametrer()
		{
			try
			{
				if (Request.QueryString["Filtro"] != null)
				{
					string filtro = Request.QueryString["Filtro"];

					DadosProdutosAtual = _produtoDAO.ListarPorDescricao(filtro);
				}
				else
				{
					DadosProdutosAtual = _produtoDAO.ListarTodos();
				}

				BindData();
			}
			catch (Exception ex)
			{
				TratarCasoDeErroAoListarProdutos(ex);
			}
		}

		private void TratarCarregamentoDeDadosViaAtualizacaoDiretaDaFonteDeDados(string filtroDescricaoProduto)
		{
			try
			{
				if (string.IsNullOrEmpty(filtroDescricaoProduto))
				{
					DadosProdutosAtual = _produtoDAO.ListarTodos();
					
				}
				else
				{
					DadosProdutosAtual = _produtoDAO.ListarPorDescricao(filtroDescricaoProduto);
				}

				BindData();
			}
			catch (Exception ex)
			{
				TratarCasoDeErroAoListarProdutos(ex);
			}
		}

		private void TratarCasoDeErroAoListarProdutos(Exception ex)
		{
			RegistroLog.Log($"Erro ao listar produtos: {ex.ToString()}");
			PageUtils.MostrarMensagemViaToastComDelay("Erro ao listar produtos", TiposMensagem.Erro, Page);
		}

		private void BindData()
		{
			ProdutosGW.DataSource = DadosProdutosAtual;
			ProdutosGW.DataBind();
		}

		protected void ProdutoFiltro_FiltrarClick(object sender, EventArgs e)
		{
			var filtro = ProdutoFiltro.Text.Trim();
			if (EhPesquiseLookUp)
			{
				TratarCarregamentoDeDadosViaAtualizacaoDiretaDaFonteDeDados(filtro);
			}
			else
			{
				//Obtem apenas a url sem possíveis query parameters no meio, desta
				//forma, o usuário não faz ter o mesmo query parameters na url
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
			
		}

		protected void ProdutosGW_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int idProdutoSelecionado = Convert.ToInt32(e.CommandArgument);
			if (idProdutoSelecionado < 0)
			{
				string menssagem = "Houve um erro ao selecionar o produto para ação selecionada, entre em contato com suporte caso o erro persista!";
				PageUtils.MostrarMensagemViaToast(menssagem, TiposMensagem.Erro, Page);
				return;
			}
			else
			{
				if (e.CommandName == "Editar")
				{
					RedirecionarParaPaginaDeEdicaoDoProduto(idProdutoSelecionado);
				}
			}
		}

		protected void NovoProdutoButton_Click(object sender, EventArgs e)
		{
			RedirecionarParaPaginaDeCadastroDoProduto();
		}

		private void RedirecionarParaPaginaDeCadastroDoProduto()
		{
			Response.Redirect("/src/pages/ProdutoPages/ProdutoPage/ProdutoPage.aspx", false);
		}

		private void RedirecionarParaPaginaDeEdicaoDoProduto(int idProduto)
		{
			Response.Redirect($"/src/pages/ProdutoPages/ProdutoPage/ProdutoPage.aspx?id={idProduto}", false);
		}

		private void ConfigurarParaPesquisaLookUp()
		{
			ProdutosTitulo.Visible = false;
			if (!PesquisaLookUpDetalhada)
			{
				ProdutosGW.Columns[2].Visible = false;
				NovoProdutoButton.Visible = false;
			}
		}

		protected void ProdutosGW_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				var produto = (Produto)e.Row.DataItem;
				e.Row.Attributes["data-id-produto"] = produto.IdProduto.ToString();
			}
		}
	}
}