<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListagemProdutos.ascx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.ListagemProdutos.ListagemProdutos" %>

<%@ Register TagPrefix="gp" TagName="Filtro" Src="~/src/components/Filtro/Filtro.ascx" %>
<%@ Register TagPrefix="gp" TagName="Titulo" Src="~/src/components/Titulo/Titulo.ascx" %>
<%@ Register TagPrefix="gp" TagName="ColunasPadraoTable" Src="~/src/components/ColunasPadraoTable/ColunasPadraoTable.ascx" %>

<link rel="stylesheet" href="/src/pages/ProdutoPages/ListagemProdutos/ListagemProdutos.css" />

<asp:UpdatePanel runat="server">
	<ContentTemplate>
		<asp:Panel runat="server" ID="ListsagemProdutoPanel">
			<gp:Titulo runat="server" Text="Produtos" ID="ProdutosTitulo"></gp:Titulo>
			<div class="filtro-container">
				<gp:Filtro ID="ProdutoFiltro" runat="server" Placeholder="Filtrar pela descrição..." OnFiltrarClick="ProdutoFiltro_FiltrarClick" />
				<asp:Button runat="server" ID="NovoProdutoButton" Text="Novo Produto" OnClick="NovoProdutoButton_Click" />
			</div>
			<div class="table-container">
				<asp:GridView
					ID="ProdutosGW"
					runat="server"
					AutoGenerateColumns="False"
					Width="100%"
					OnRowCommand="ProdutosGW_RowCommand"
					OnRowDataBound="ProdutosGW_RowDataBound"
					CssClass='<%# EhPesquiseLookUp ? "table-pesquisa-look-up" : "" %>'>
					<HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
					<Columns>
						<asp:BoundField DataField="Descricao" HeaderText="Descrição" />
						<asp:BoundField DataField="VlrUnitario" HeaderText="Valor Unitário" DataFormatString="{0:C}" />
						<asp:TemplateField>
							<ItemTemplate>
								<div style="display: flex; justify-content: space-evenly">
									<gp:ColunasPadraoTable runat="server" ID="ProdutoColunasPadraoTable" IdRegistro='<%# ((ModelsGerenciamentoPedidos.Src.Produto)Container.DataItem).IdProduto %>'
										MensagemConfirmacaoExclusao='<%# "Tem certeza que deseja excluir o produto \"" + ((ModelsGerenciamentoPedidos.Src.Produto)Container.DataItem).Descricao + "\"?" %>'
										UrlMetodoExclusao="ProdutosNovo.aspx/ExcluirProduto" />
								</div>
							</ItemTemplate>
							<ItemStyle Width="200px" />
						</asp:TemplateField>
					</Columns>
					<EmptyDataTemplate>
						<div class="empty-data">
							Nenhum produto encontrado.
						</div>
					</EmptyDataTemplate>
				</asp:GridView>
			</div>
		</asp:Panel>
	</ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
	var myApp = myApp || {};

	myApp.ListagemProdutos = {
		ScriptAoSelecionarProduto: <%= string.IsNullOrEmpty(ScriptAoSelecionarProduto) ? "() => {}" : ScriptAoSelecionarProduto %>
	};
	
</script>

<script type="text/javascript" src="/src/pages/ProdutoPages/ListagemProdutos/ListagemProdutos.js"> </script>
