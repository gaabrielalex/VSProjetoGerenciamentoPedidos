﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.PedidoPages.Pedidos.Pedidos" %>

<%@ Register TagPrefix="gp" TagName="Filtro" Src="~/src/components/Filtro/Filtro.ascx" %>
<%@ Register TagPrefix="gp" TagName="FormAddEditPedido" Src="~/src/pages/PedidoPages/FormAddEditPedido/FormAddEditPedido.ascx" %>
<%@ Register TagPrefix="gp" TagName="Titulo" Src="~/src/components/Titulo/Titulo.ascx" %>
<%@ Register TagPrefix="gp" TagName="ColunasPadraoTable" Src="~/src/components/ColunasPadraoTable/ColunasPadraoTable.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<link rel="stylesheet" href="Pedido.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="modal fade" id="modalObservacaoPedido" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<h1 class="modal-title fs-5">Observações</h1>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body">
					...
				</div>
				<div class="modal-footer">
				</div>
			</div>
		</div>
	</div>

	<gp:FormAddEditPedido runat="server" id="FormAddEditPedido" />

	<asp:UpdatePanel runat="server">
		<ContentTemplate>
			<asp:Panel runat="server" ID="ListsagemPedidoPanel">
				<gp:Titulo runat="server" Text="Pedidos" ID="PedidosTitulo"></gp:Titulo>
				<div class="filtro-container">
					<gp:Filtro ID="PedidoFiltro" runat="server" Placeholder="Filtrar pelo cliente..." OnFiltrarClick="ProdutoFiltro_FiltrarClick" />
					<asp:Button runat="server" ID="NovoPedidoButton" Text="Novo Pedido" OnClick="NovoPedidoButton_Click" />
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
							<asp:BoundField DataField="MetodoPagamento.Descricao" HeaderText="Mtd Pagto" />
							<asp:BoundField DataField="DtHrPedido" HeaderText="Data/hora" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
							<asp:BoundField DataField="DescricaoStatusPedido" HeaderText="Status" />

							<asp:TemplateField>
								<ItemTemplate>
									<div style="display: flex; justify-content: space-evenly">
										<asp:LinkButton ID="ObservacoesLK" runat="server"
											Text="Observações" CausesValidation="False"
											OnClientClick='<%# "return myApp.abrilModalObservacoesPedido(\"" + ((ModelsGerenciamentoPedidos.Src.Pedido)Container.DataItem).Observacoes.ToString().Replace("\"", "\\\"") + "\"); return false;" %>'
											CssClass='<%# String.IsNullOrEmpty(((ModelsGerenciamentoPedidos.Src.Pedido)Container.DataItem).Observacoes.ToString()) ? "hidden-link" : "" %>'
											>
										</asp:LinkButton>
										<gp:ColunasPadraoTable runat="server" ID="PedidoColunasPadraoTable" IdRegistro='<%# ((ModelsGerenciamentoPedidos.Src.Pedido)Container.DataItem).IdPedido %>'
											MensagemConfirmacaoExclusao='<%# "Tem certeza que deseja excluir o pedido do(a) cliente \"" + ((ModelsGerenciamentoPedidos.Src.Pedido)Container.DataItem).NomeCliente + "\"?" %>'
											UrlMetodoExclusao="Pedidos.aspx/ExcluirPedido" />
									</div>
								</ItemTemplate>
								<ItemStyle Width="300px" />
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
	<script type="text/javascript" src="Pedido.js"> </script>
</asp:Content>
