<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProdutosNovo.aspx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.Produtos_Novo_.ProdutosNovo" %>

<%@ Register TagPrefix="gp" TagName="Filtro" Src="~/src/components/Filtro/Filtro.ascx" %>
<%@ Register TagPrefix="gp" TagName="FormAddEditProduto" Src="~/src/pages/ProdutoPages/FormAddEditProduto/FormAddEditProduto.ascx" %>
<%@ Register TagPrefix="gp" TagName="Titulo" Src="~/src/components/Titulo/Titulo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
	<style>
		.filtro-container {
			display: flex;
			justify-content: space-between;
		}

		.table-container {
			width: 100%;
			height: 70vh;
			overflow-x: auto;
			margin-top: 25px;
		}

			.table-container .table th, .table td {
				padding: 10px;
				border: 1px solid #ddd;
			}

			.table-container td {
				border-bottom: 1px solid #ccc;
			}
	</style>
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
								<asp:BoundField DataField="VlrUnitario" HeaderText="Valor Unitário" />
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
	<script type="text/javascript">

		//Deixei tudo isso de lado por enquanto
		//$(document).ready(function () {
		//	$(".excluirLK").click(function (event) {
		//		event.preventDefault(); // Evita o comportamento padrão do link que seria realizar o postBack
		//		__doPostBack("ExcluirProduto", "");
		//		const nomeMetodoParaRealizarExclusao = "ExcluirProduto";
		//		const idRegistro = $(this).data("idproduto");
		//		const descricaoRegistro = $(this).data("descricao");
		//		abriModalConfirmacaoExclusaoComPostBack({ nomeMetodoParaRealizarExclusao, idRegistro, descricaoRegistro });
		//	});
		//});
		//$(document).ready(function () {
		//	$(".excluirLK").click(function (event) {
		//		event.preventDefault(); // Evita o comportamento padrão do link que seria realizar o postBack
		//		const idRegistro = $(this).data('id-produto');
		//		const descricaoRegistro = $(this).data('descricao');
		//		const urlMetodo = "ProdutosNovo.aspx/ExcluirProduto";
		//		const callbackSucesso = () => {
		//			showToast({ message: "Produto excluído com sucesso", type: "s" });
		//			delay(2500).then(function () {
		//				__doPostBack('DepoisDeExcluirProduto', '');
		//			});
		//		}
		//		const callbackErro = (xhr, status, error) => {
		//			var errorPayload = JSON.parse(xhr.responseText);
		//			showToast({
		//				message: `Houve um erro ao excluir o produto: ${errorPayload.ExceptionType} - ${errorPayload.Message}`,
		//				type: "e"
		//			})
		//		}

		//		abriModalConfirmacaoExclusaoComAjax(
		//			{ idRegistro, descricaoRegistro, urlMetodo, callbackSucesso, callbackErro }
		//		);

		//	});

		//	function delay(ms) {
		//		return new Promise(resolve => setTimeout(resolve, ms));
		//	}

		//});
	</script>
</asp:Content>
