<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.PedidoPages.Pedidos.Pedidos" %>

<%@ Register TagPrefix="gp" TagName="Filtro" Src="~/src/components/Filtro/Filtro.ascx" %>
<%@ Register TagPrefix="gp" TagName="FormAddEditProduto" Src="~/src/pages/ProdutoPages/FormAddEditProduto/FormAddEditProduto.ascx" %>
<%@ Register TagPrefix="gp" TagName="Titulo" Src="~/src/components/Titulo/Titulo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<link rel="stylesheet" href="Pedido.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<%--<gp:FormAddEditProduto runat="server" ID="FormAddEditProduto" />--%>

	<asp:UpdatePanel runat="server">
		<ContentTemplate>
			<asp:Panel runat="server" ID="ListsagemPedidoPanel">
				<gp:Titulo runat="server" Text="Pedidos" ID="PedidosTitulo"></gp:Titulo>
				<div class="filtro-container">
					<gp:Filtro ID="PedidoFiltro" runat="server" Placeholder="Filtrar pelo cliente..." OnFiltrarClick="ProdutoFiltro_FiltrarClick" />
					<%--<asp:Button runat="server" ID="NovoPedidoButton" Text="Novo Pedido" OnClick="NovoPedidoButton_Click" />--%>
				</div>
				<div class="table-container">
					<asp:GridView
						ID="PedidosGW"
						runat="server"
						AutoGenerateColumns="False"
						Width="100%"
						OnRowCommand="PedidosGW_RowCommand">
						<HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
						<Columns>
							<asp:BoundField DataField="NomeCliente" HeaderText="Cliente" />
							<asp:BoundField DataField="VlrSubtotal" HeaderText="Vlr Subtotal" DataFormatString="{0:C}" />
							<asp:BoundField DataField="Desconto" HeaderText="Desconto" DataFormatString="{0:C}" />
							<asp:BoundField DataField="VlrTotal" HeaderText="Vlr Total" DataFormatString="{0:C}" />
							<asp:BoundField DataField="DtHrPedido" HeaderText="Data/hora" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
							<asp:BoundField DataField="DescricaoStatusPedido" HeaderText="Status" /

							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="EditarLK" runat="server" CommandName="Detalhar"
										CommandArgument='<%# Eval("IdPedido") %>' Text="Detalhar" CausesValidation="False">
									</asp:LinkButton>
								</ItemTemplate>
								<ItemStyle Width="100px" />
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="EditarLK" runat="server" CommandName="Editar"
										CommandArgument='<%# Eval("IdPedido") %>' Text="Editar" CausesValidation="False">
									</asp:LinkButton>
								</ItemTemplate>
								<ItemStyle Width="100px" />
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="ExcluirLK" runat="server" CommandName="Excluir" CommandArgument='<%# Eval("IdPedido") %>'
										Text="Excluir" CssClass="excluirLK" data-id-pedido='<%# Eval("IdPedido") %>' data-cliente='<%# Eval("NomeCliente") %>'>
									</asp:LinkButton>
								</ItemTemplate>
								<ItemStyle Width="100px" />
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="EditarLK" runat="server" CommandName="Selecionar"
										CommandArgument='<%# Eval("IdPedido") %>' Text="Selecionar" CausesValidation="False">
									</asp:LinkButton>
								</ItemTemplate>
								<ItemStyle Width="100px" />
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>

	<script type="text/javascript" src="pedido.js"> </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
