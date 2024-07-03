
var myApp = myApp || {};

(function () {
	myApp.ServicoMensagensAoCarregarPaginas = {
		adicionarMensagem: function (messagem, tipo) {
			localStorage.setItem('menssagemParaSerExibida', JSON.stringify({
				mensagem: messagem,
				tipo: tipo
			}));
		},

		exibirMensagem: function () {
			var mensagem = localStorage.getItem('menssagemParaSerExibida');

			if (mensagem) {
				var mensagemObj = JSON.parse(mensagem);
				myApp.showToast(mensagemObj.mensagem, mensagemObj.tipo);
				localStorage.removeItem('menssagemParaSerExibida');
			}
		}
	};

	// Aguarda até que todos os recursos sejam carregados
	window.onload = function () {
		// Após a página ser completamente carregada, exibe a mensagem
		myApp.ServicoMensagensAoCarregarPaginas.exibirMensagem();
	};
})();

