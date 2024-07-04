<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PesquisaLookUpProdutos.ascx.cs" Inherits="WebGereciamentoPedidos.src.pages.ProdutoPages.PesquisaLookUpProdutos.PesquisaLookUpProdutos" %>

<%@ Register TagPrefix="gp" TagName="ListagemProdutos" Src="~/src/pages/ProdutoPages/ListagemProdutos/ListagemProdutos.ascx" %>

<div class="modal fade" id="modalPesquisaLookUpProdutos" tabindex="-1" aria-hidden="true">
	<div class="modal-dialog modal-lg">
		<div class="modal-content">
			<div class="modal-header">
				<h1 class="modal-title fs-5">Pesquisar Produto</h1>
				<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
			</div>
			<div class="modal-body">
				<gp:ListagemProdutos runat="server" ID="ListagemProdutos" EhPesquiseLookUp="true" PesquisaLookUpDetalhada="false" />
			</div>
			<div class="modal-footer">
			</div>
		</div>
	</div>
</div>

<script type="text/javascript" src="/src/pages/ProdutoPages/PesquisaLookUpProdutos/PesquisaLookUpProdutos.js"> </script>


