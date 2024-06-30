<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListagemItensDoPedido.ascx.cs" Inherits="WebGereciamentoPedidos.src.pages.ItemPedidoPages.ListagemItensDoPedido.ListagemItensDoPedido" %>

<%@ Register TagPrefix="gp" TagName="TituloMedio" Src="~/src/components/TituloMedio/TituloMedio.ascx" %>
<%@ Register TagPrefix="gp" TagName="ColunasPadraoTable" Src="~/src/components/ColunasPadraoTable/ColunasPadraoTable.ascx" %>

<link rel="stylesheet" href="/src/pages/ItemPedidoPages/ListagemItensDoPedido/ListagemItensDoPedido.css" />

<asp:UpdatePanel runat="server">
	<ContentTemplate>
		<div style="width:100%">
			<gp:TituloMedio runat="server" text="Itens" id="ItensPedidoTitulo"></gp:TituloMedio>
			<div class="table-container">
				<asp:GridView
					ID="ItensPedidosGW"
					runat="server"
					AutoGenerateColumns="False"
					Width="100%"
					OnRowCommand="ItensPedidosGW_RowCommand">
					<HeaderStyle BackColor="#212529" ForeColor="White" Font-Bold="True" />
					<Columns>
						<asp:BoundField DataField="Produto.Descricao" HeaderText="Produto" />
						<asp:BoundField DataField="Produto.VlrUnitario" HeaderText="Vlr Unitário" DataFormatString="{0:C}" />
						<asp:BoundField DataField="Quantidade" HeaderText="Qtde" />
						<asp:BoundField DataField="VlrTotalItem" HeaderText="Vlr Total" DataFormatString="{0:C}" />
						<asp:TemplateField>
							<ItemTemplate>
								<div style="display: flex; justify-content: space-evenly">
									<gp:ColunasPadraoTable runat="server" id="ItensPedidoColunasPadraoTable" idregistro='<%# ((ModelsGerenciamentoPedidos.Src.ItemPedido)Container.DataItem).IdItemPedido %>'
										mensagemconfirmacaoexclusao='<%# "Tem certeza que deseja excluir o item \"" + ((ModelsGerenciamentoPedidos.Src.ItemPedido)Container.DataItem).Produto.Descricao + "\"?" %>'
										urlmetodoexclusao="PedidoPage.aspx/ExcluirItemPedido" />
								</div>
							</ItemTemplate>
							<ItemStyle Width="200px" />
						</asp:TemplateField>
					</Columns>
					<EmptyDataTemplate>
						<div class="empty-data">
							O pedido não possui itens.
						</div>
					</EmptyDataTemplate>
				</asp:GridView>
			</div>
		</div>
	</ContentTemplate>
</asp:UpdatePanel>