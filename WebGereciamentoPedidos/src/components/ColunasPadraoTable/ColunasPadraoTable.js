
var myApp = myApp || {};

(function () {
	document.getElementById(myApp.idExcluirLK).addEventListener('click', function (event) {
		event.preventDefault(); // Evita o comportamento padrão do link que seria realizar o postBack
		const idRegistro = $(this).data('id-registro');
		const mensagemConfirmacaoExclusao = $(this).data('mensagem-confirmacao-exclusao');
		const urlMetodo = $(this).data('url-metodo-exclusao');
		const callbackSucesso = () => {
			myApp.ServicoMensagensAoCarregarPaginas.adicionarMensagem("Registro excluído com sucesso", "s");
			window.location.reload();
		}
		const callbackErro = (xhr, status, error) => {
			myApp.showToast(
				`Houve um erro ao excluir o registro`,
				"e"
			)
		}

		myApp.abriModalConfirmacaoExclusaoComAjax(
			{ idRegistro, mensagemConfirmacaoExclusao, urlMetodo, callbackSucesso, callbackErro }
		);
	});
})();