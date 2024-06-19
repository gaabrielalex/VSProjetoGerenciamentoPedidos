
var myApp = myApp || {};

(function(){
	myApp.fecharModal = function() {
		$('#modalConfirmacaoExclusao').modal('hide');
	}

	myApp.abriModalConfirmacaoExclusaoComAjax = function({ idRegistro, mensagemConfirmacaoExclusao, urlMetodo, callbackSucesso, callbackErro, complete }) {
		const dados = {
			id: idRegistro,
		}
		const beforeSend = myApp.fecharModal;

		$('#modalConfirmacaoExclusao .modal-body').text(mensagemConfirmacaoExclusao);
		$('#modalConfirmacaoExclusao .btn-sim').off('click').on('click', () => myApp.confirmarExclusaoComAjax(
			{ urlMetodo, dados, beforeSend, callbackSucesso, callbackErro, complete }
		));
		$('#modalConfirmacaoExclusao').modal('show');
		return false; // Retorne false para evitar postback
	}

	myApp.confirmarExclusaoComAjax = function({ urlMetodo, dados, beforeSend, callbackSucesso, callbackErro, complete }) {
		$.ajax({
			type: "POST",
			url: urlMetodo,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: JSON.stringify(dados),
			beforeSend: beforeSend,
			success: callbackSucesso,
			error: callbackErro,
			complete: complete
		});
	}
}) ();