
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
		if (myApp.utils.converterParaFloatUmaStringEmFormatoBr(inputDesconto.value) > myApp.utils.converterParaFloatUmaStringEmFormatoBr(inputValorSubTotal.value)) {
			spanValidacaoDesconto.textContent = 'Desconto não pode ser maior que o valor subtotal!';
			spanValidacaoDesconto.style.visibility = 'visible';
		} else {
			spanValidacaoDesconto.style.visibility = 'hidden';
		}
	});
})();