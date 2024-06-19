
var myApp = myApp || {};

(function () {
	$(document).ready(function () {
		$(".produto-excluirLK").click(function (event) {
			event.preventDefault(); // Evita o comportamento padrão do link que seria realizar o postBack
			const idRegistro = $(this).data('id-produto');
			const mensagemConfirmacaoExclusao = $(this).data('mensagem-confirmacao-exclusao');
			const urlMetodo = "ProdutosNovo.aspx/ExcluirProduto";
			const callbackSucesso = () => {
				myApp.showToast("Produto excluído com sucesso", "s");
				myApp.utils.delay(1000).then(() => {
					window.location.reload();
				});
			}
			const callbackErro = (xhr, status, error) => {
				myApp.showToast(
					`Houve um erro ao excluir o produto`,
					"e"
				)
			}

			myApp.abriModalConfirmacaoExclusaoComAjax(
				{ idRegistro, mensagemConfirmacaoExclusao, urlMetodo, callbackSucesso, callbackErro }
			);

		});
	});
})();