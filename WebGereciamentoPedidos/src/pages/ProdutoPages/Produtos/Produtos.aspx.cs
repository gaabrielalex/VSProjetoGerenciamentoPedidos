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
			}
		}
		private void TratarCarregamentoDeDados() {
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
							try {
								ProdutoDAO.excluir(produtoADeletar.IdProduto ?? 0);
								TratarCarregamentoDeDados();
							} catch (Exception ex) {
								PageUtils.mostrarMensagem($"Erro ao deletar produto: {ex.Message}", "E", this);

							} finally
							{
								//Se cair no catch ele não vai executar esse função que esta tb sendo acionado no final
								//dessa função, desta forme o mesmo deve ser chamado aqui tb
								PageUtils.RedirecionarClienteParaEvitarResubimissaoDeFormulario(Response, Request, Context);
							}
						}
					}
				}

			PageUtils.RedirecionarClienteParaEvitarResubimissaoDeFormulario(Response, Request, Context);
		}

		protected void FiltrarButton_Click(object sender, EventArgs e)
		{
			String filtro = FiltrarTextBox.Text;
			//Obtem apenas a url sem possíveis query parameters no meio, desta forma, o usuário não faz ter o mesmo query parametres na url
			String urlAtual = Request.Url.GetLeftPart(UriPartial.Path);
			String novaUrl;
			if (filtro == "" || filtro == null)
			{
				novaUrl = urlAtual;
			} else {
				novaUrl = urlAtual + $"?filtro={filtro}";
			}

			Response.Redirect(novaUrl, false);
		}
	}
}