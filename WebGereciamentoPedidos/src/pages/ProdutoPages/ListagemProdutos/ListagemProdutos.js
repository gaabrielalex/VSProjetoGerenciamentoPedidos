
var myApp = myApp || {};

(function () {
	$(document).ready(function () {
		$('.table-pesquisa-look-up tbody tr').on('click', function () {
			var idProduto = $(this).data('id-produto');
			myApp.ListagemProdutos.ScriptAoSelecionarProduto(idProduto);
		});
	});
})();