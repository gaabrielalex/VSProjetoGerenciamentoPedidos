<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProdutosNovo.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos_Novo_.ProdutosNovo" %>

<%@ Register TagPrefix="gp" TagName="Filtro" Src="~/src/components/Filtro/Filtro.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<style>
		.title-container {
			margin: 40px 0;
		}

		.table-container {
			width: 100%;
			height: 55vh;
			overflow-x: auto;
			margin-top: 25px;
		}

		.table-container .table th, .table td {
			padding: 10px;
			border: 1px solid #ddd;
		}

		.table-container td {
			border-bottom: 1px solid #ccc; /* Adjust color and style as needed */
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="title-container">
		<h1 runat="server">
			<asp:Label ID="TituloPanelLabel" runat="server" Text="Cadastrar Produto"></asp:Label>
		</h1>
	</div>
	<div class="filtro-container">
		<gp:Filtro ID="Filtro" runat="server" Placeholder="Filtra pela descrição..." />
	</div>
	<div class="table-container">
		<table class="table table-striped " cellspacing="1" cellpadding="1" width="100%" border="0">
			<tr>
				<td>
					<asp:Label ID="lblTitulo" runat="server" Text="Produtos"></asp:Label>
				</td>
			</tr>
			<asp:GridView
				ID="ProdutosGW"
				runat="server"
				AutoGenerateColumns="False"
				Width="100%"
				AllowPaging="false">
				<HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
				<Columns>
					<asp:BoundField DataField="Descricao" HeaderText="Descrição" />
					<asp:BoundField DataField="VlrUnitario" HeaderText="Valor Unitário" />
					<asp:TemplateField>
						<ItemTemplate>
							<asp:LinkButton ID="EditarLK" runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdProduto") %>'
								Text="Editar">
							</asp:LinkButton>
						</ItemTemplate>
						<ItemStyle Width="100px" />
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:LinkButton ID="ExcluirLK" runat="server" CommandName="Excluir" CommandArgument='<%# Eval("IdProduto") %>'
								Text="Excluir" CssClass="excluirLK" data-idproduto='<%# Eval("IdProduto") %>' data-descricao='<%# Eval("Descricao") %>'>
							</asp:LinkButton>
						</ItemTemplate>
						<ItemStyle Width="100px" />
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</table>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
