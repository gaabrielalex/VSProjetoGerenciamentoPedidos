
var myApp = myApp || {};

(function () {
	myApp.PesquisaLookUpProdutos = {
		abrirModalPesquisa: function () {
			$("#modalPesquisaLookUpProdutos").modal("show");
		},
		fecharModalPesquisa: function () {
			$("#modalPesquisaLookUpProdutos").modal("hide");
		}
	};
})();

