using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WebGereciamentoPedidos.src.dao;
using WebGereciamentoPedidos.src.models;
using WebGereciamentoPedidos.src.pages.ProdutoPages;
using WebGereciamentoPedidos.src.util;

namespace WebGereciamentoPedidos.src.pages.ProdutoPages
{
	public partial class Produtos : System.Web.UI.Page
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

			//Quando eu coloco esse ação aqui para chmar minha função que mostra o toast ele aparece no console do browser que a função não foi encontrada(not defined)
			//Page.ClientScript.RegisterStartupScript(typeof(Page), "showToast", "showToast('Erro ao deletar produto.', 'error');", true);
			//Não funciona tb, nem isso
			//Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "showModalMessage", "alert('teste');", true);

			if (!IsPostBack)
			{
				TratarCarregamentoDeDados();
				CarregarEstadoDeVariaveisEComponentesNecesarios();
			}
		}
		private void TratarCarregamentoDeDados()
		{
			if (Request.QueryString["Filtro"] != null)
			{
				string filtro = Request.QueryString["Filtro"];

				DadosProdutosAtual = ProdutoDAO.listarPorDescricao(filtro);
			}
			else
			{
				DadosProdutosAtual = ProdutoDAO.listar();
			}
			BindData();
		}
		private void BindData()
		{
			dgProdutos.DataSource = DadosProdutosAtual;
			dgProdutos.DataBind();
		}

		protected void dgProdutos_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			//Teste: da erro
			//Page.ClientScript.RegisterStartupScript(typeof(Page), "showToast", "showToast('Erro ao deletar produto.', 'error');", true);
			if (e.CommandName == "Delete")
			{
				int indice = e.Item.ItemIndex;
				if (indice < 0)
				{
					PageUtils.mostrarMensagem("Houve um erro ao slecionar o produto para exclusão, entre em contato com suporte caso o erro persista.", "E", this);
				}
				else
				{
					Produto produtoADeletar = DadosProdutosAtual[indice];
					bool confirmacaoExclusao = PageUtils.solicitarConfirmacao($"Deseja realmente excluir o produto \"{produtoADeletar.Descricao}\"?");

					if (confirmacaoExclusao)
					{
						try
						{
							ProdutoDAO.excluir(produtoADeletar.IdProduto ?? 0);
							TratarCarregamentoDeDados();
						}
						catch (Exception ex)
						{
							PageUtils.mostrarMensagem($"Erro ao deletar produto: {ex.Message}", "E", this);

						}
						finally
						{
							//Se cair no catch ele não vai executar esse função que esta tb sendo acionado no final
							//dessa função, desta forme o mesmo deve ser chamado aqui tb
							ImpedirResubimissaoDeFormulario(Response, Request, Context);
						}
					}
				}
			}

			ImpedirResubimissaoDeFormulario(Response, Request, Context);
		}

		protected void FiltrarButton_Click(object sender, EventArgs e)
		{
			String filtro = FiltrarTextBox.Text;
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
		protected void CadastrarProdutoLinkButton_Click(object sender, EventArgs e)
		{
			CamposProdutoPanel.Visible = !CamposProdutoPanel.Visible;
		}
		private void ArmazenarEstadoDeVariaveisEComponentesNecesarios()
		{
			Session["CamposProdutoPanelVisible"] = CamposProdutoPanel.Visible;
			Session["DescricaoProdutoTxtBoxText"] = DescricaoProdutoTxtBox.Text;
			Session["VlrUnitarioProdutoTxtBoxText"] = VlrUnitarioProdutoTxtBox.Text;
		}
		private void CarregarEstadoDeVariaveisEComponentesNecesarios()
		{
			// Verificar e carregar o estado do painel
			if (Session["CamposProdutoPanelVisible"] != null)
			{
				CamposProdutoPanel.Visible = (bool)Session["CamposProdutoPanelVisible"];
			}
			else
			{
				CamposProdutoPanel.Visible = true; // Valor padrão
			}

			// Verificar e carregar o texto da descrição do produto
			if (Session["DescricaoProdutoTxtBoxText"] != null)
			{
				DescricaoProdutoTxtBox.Text = Session["DescricaoProdutoTxtBoxText"].ToString();
			}
			else
			{
				DescricaoProdutoTxtBox.Text = ""; // Valor padrão
			}

			// Verificar e carregar o texto do valor unitário do produto
			if (Session["VlrUnitarioProdutoTxtBoxText"] != null)
			{
				VlrUnitarioProdutoTxtBox.Text = Session["VlrUnitarioProdutoTxtBoxText"].ToString();
			}
			else
			{
				VlrUnitarioProdutoTxtBox.Text = ""; // Valor padrão
			}
		}

		private void ImpedirResubimissaoDeFormulario(HttpResponse response, HttpRequest request, HttpContext context)
		{
			//A técnica usada exige que gerenciamos o estado de componentes
			//e variáveis necessáris através de recurso "Session"
			//O usuário é redirecionado para que seja impedido a resubimissao do formulario
			PageUtils.RedirecionarClienteParaEvitarResubimissaoDeFormulario(Response, Request, Context);
			//Porém, nesse momento o estado da página é perdido, o que não era a intenção
			//Desta forma, realizamos o gerenciamento de estado "manualmente"
			ArmazenarEstadoDeVariaveisEComponentesNecesarios();
			//No paload, se não for postback, carregamos novamente o estado anteior da página
			//recuperrabdo as informações via seesion
		}
	}
}