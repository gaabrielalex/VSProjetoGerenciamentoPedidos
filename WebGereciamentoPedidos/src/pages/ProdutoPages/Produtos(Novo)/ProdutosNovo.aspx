<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProdutosNovo.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos_Novo_.ProdutosNovo" %>

<%@ Register TagPrefix="gp" TagName="Filtro" Src="~/src/components/Filtro/Filtro.ascx" %>
<%@ Register TagPrefix="gp" TagName="FormAddEditProduto" Src="~/src/pages/ProdutoPages/FormAddEditProduto/FormAddEditProduto.ascx" %>
<%@ Register TagPrefix="gp" TagName="Titulo" Src="~/src/components/Titulo/Titulo.ascx" %>
<%@ Register TagPrefix="gp" TagName="ColunasPadraoTable" Src="~/src/components/ColunasPadraoTable/ColunasPadraoTable.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<link rel="stylesheet" href="ProdutosNovo.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<gp:FormAddEditProduto runat="server" ID="FormAddEditProduto" />

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
						OnRowCommand="ProdutosGW_RowCommand">
						<HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
						<Columns>
							<asp:BoundField DataField="Descricao" HeaderText="Descrição" />
							<asp:BoundField DataField="VlrUnitario" HeaderText="Valor Unitário" DataFormatString="{0:C}" />
							<asp:TemplateField>
								<ItemTemplate>
									<gp:ColunasPadraoTable runat="server" Id="ProdutoColunasPadraoTable" IdRegistro='<%# Eval("IdProduto") %>'
									MensagemConfirmacaoExclusao='<%# "Tem certeza que deseja excluir o produto \"" + Eval("Descricao") + "\"?" %>'
									UrlMetodoExclusao="ProdutosNovo.aspx/ExcluirProduto"
									/>
								</ItemTemplate>
								<ItemStyle Width="200px" />
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
	<script type="text/javascript" src="ProdutoNovo.js"> </script>
</asp:Content>
