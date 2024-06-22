
var myApp = myApp || {};

(function () {
	let inputValorSubTotal = document.getElementById(myApp.idInputVlrSubtotal);
	let inputDesconto = document.getElementById(myApp.idInputDesconto);
	let inputValorTotal = document.getElementById(myApp.idInputVlrTotal);
	let spanValidacaoDesconto = document.getElementById(myApp.idSpanValidacaoDesconto);

	inputDesconto.addEventListener('focus', function () {
		if (parseFloat(inputDesconto.value) === 0) {
			inputDesconto.value = '';
		}
	});

	inputDesconto.addEventListener('blur', function () {
		if (inputDesconto.value.trim() === '') {
			inputDesconto.value = '00,00';
		}
	});

	//Desconto não pode ser maior que o valor do subtotal
	inputDesconto.addEventListener('change', function () {
		let valorSubTotal = myApp.utils.converterParaFloatUmaStringEmFormatoBr(inputValorSubTotal.value);
		let desconto = myApp.utils.converterParaFloatUmaStringEmFormatoBr(inputDesconto.value);

		if (desconto > valorSubTotal) {
			spanValidacaoDesconto.textContent = 'Desconto não pode ser maior que o valor subtotal!';
			spanValidacaoDesconto.style.visibility = 'visible';
		} else {
			spanValidacaoDesconto.style.visibility = 'hidden';

			inputValorTotal.value = myApp.utils.converterParaStringEmFormatoBrUmFLoat(valorSubTotal - desconto);
		}
	});
	
})();