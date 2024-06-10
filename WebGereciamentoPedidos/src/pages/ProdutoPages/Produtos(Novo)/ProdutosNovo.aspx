<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProdutosNovo.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos_Novo_.ProdutosNovo" %>

<%@ Register TagPrefix="gp" TagName="Filtro" Src="~/src/components/Filtro/Filtro.ascx" %>
<%@ Register TagPrefix="gp" TagName="FormAddEditProduto" Src="~/src/pages/ProdutoPages/FormAddEditProduto/FormAddEditProduto.ascx" %>
<%@ Register TagPrefix="gp" TagName="Titulo" Src="~/src/components/Titulo/Titulo.ascx" %>

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
					<gp:Filtro ID="ProdutoFiltro" runat="server" Placeholder="Filtra pela descrição..." OnFiltrarClick="ProdutoFiltro_FiltrarClick" />
					<asp:Button runat="server" ID="NovoProdutoButton" Text="Novo Produto" OnClick="NovoProdutoButton_Click" />
				</div>
				<div class="table-container">
					<table class="table">

						<asp:GridView
							ID="ProdutosGW"
							runat="server"
							AutoGenerateColumns="False"
							Width="100%"
							AllowPaging="false"
							OnRowCommand="ProdutosGW_RowCommand">
							<HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
							<Columns>
								<asp:BoundField DataField="Descricao" HeaderText="Descrição" />
								<asp:BoundField DataField="VlrUnitario" HeaderText="Valor Unitário" DataFormatString="{0:C}" />
								<asp:TemplateField>
									<ItemTemplate>
										<asp:LinkButton ID="EditarLK" runat="server" CommandName="Editar"
											CommandArgument='<%# Eval("IdProduto") %>' Text="Editar" CausesValidation="False">
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="100px" />
								</asp:TemplateField>
								<asp:TemplateField>
									<ItemTemplate>
										<asp:LinkButton ID="ExcluirLK" runat="server" CommandName="Excluir" CommandArgument='<%# Eval("IdProduto") %>'
											Text="Excluir" CssClass="excluirLK" data-id-produto='<%# Eval("IdProduto") %>' data-descricao='<%# Eval("Descricao") %>'>
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="100px" />
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</table>
				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>

	<script type="text/javascript" src="produto-novo.js"> </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
