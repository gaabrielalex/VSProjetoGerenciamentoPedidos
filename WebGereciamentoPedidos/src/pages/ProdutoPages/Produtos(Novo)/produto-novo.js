
var myApp = myApp || {};

(function () {
	$(document).ready(function () {
		$(".excluirLK").click(function (event) {
			event.preventDefault(); // Evita o comportamento padrão do link que seria realizar o postBack
			const idRegistro = $(this).data('id-produto');
			const descricaoRegistro = $(this).data('descricao');
			const urlMetodo = "ProdutosNovo.aspx/ExcluirProduto";
			const callbackSucesso = () => {
				myApp.showToast("Produto excluído com sucesso", "s");
				delay(1000).then(function () {
					window.location.reload();

				});
			}
			const callbackErro = (xhr, status, error) => {
				var errorPayload = JSON.parse(xhr.responseText);
				myApp.showToast(
					`Houve um erro ao excluir o produto`,
					"e"
				)
				//myApp.showToast(
				//	`Houve um erro ao excluir o produto: ${errorPayload.ExceptionType} - ${errorPayload.Message}`,
				//	"e"
				//)
			}

			myApp.abriModalConfirmacaoExclusaoComAjax(
				{ idRegistro, descricaoRegistro, urlMetodo, callbackSucesso, callbackErro }
			);

		});

		function delay(ms) {
			return new Promise(resolve => setTimeout(resolve, ms));
		}

	});
})();