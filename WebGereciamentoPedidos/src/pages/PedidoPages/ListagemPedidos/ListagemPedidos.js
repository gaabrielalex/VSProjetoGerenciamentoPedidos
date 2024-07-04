
var myApp = myApp || {};

(function () {
	myApp.abrilModalObservacoesPedido = (observacoes) => {
		if (observacoes == null || observacoes == '' || observacoes == undefined) {
			observacoes = 'Não há observações';
		}
		document.querySelector('#modalObservacaoPedido .modal-body').innerHTML = observacoes;
		$('#modalObservacaoPedido').modal('show');
	}
})();