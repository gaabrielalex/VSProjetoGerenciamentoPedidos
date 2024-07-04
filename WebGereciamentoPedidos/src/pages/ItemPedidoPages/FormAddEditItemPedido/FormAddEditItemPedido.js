
var myApp = myApp || {};

(function () {
	myApp.FormAddEditItemPedido = {
		carregarInformacoesProduto: function (idProduto) {
			$.ajax({
				type: "POST",
				contentType: "application/json; charset=utf-8",
				url: "PedidoPage.aspx/ObterProdutoPorId",
				dataType: "json",
				data: JSON.stringify({ idProduto: idProduto }),
				success: function (data) {
					dataJson = JSON.parse(data.d);
					produto = dataJson.Produto;
					if (produto == null) {
						myApp.showToast("Erro ao carregar informações do produto", "e");
						console.log("O produto não foi encontrado");
					}
					$('.CampoDescricaoProduto input').val(produto.Descricao);
					inputIdProduto = document.getElementById(myApp.FormAddEditItemPedido.InputHiddenIdProduto);
					inputIdProduto.value = produto.IdProduto;

					myApp.PesquisaLookUpProdutos.fecharModalPesquisa();
				},
				error: function () {
					myApp.showToast("Erro ao carregar informações do produto", "e");
				}
			});
		}
	}
})();