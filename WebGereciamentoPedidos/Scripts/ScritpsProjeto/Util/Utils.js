
var myApp = myApp || {};

(function () {
	myApp.utils = {
		delay : function(ms) {
			return new Promise(resolve => setTimeout(resolve, ms));
		},
		retirarPontosDeUmNumero: function (numero) {
			return numero.replace(/\./g, '');
		},
		converterParaFloatUmaStringEmFormatoBr: function (numeroString) {
			numeroString = numeroString.replace(/\./g, '');
			numeroString = numeroString.replace(',', '.');
			return parseFloat(numeroString);
		},
	};
})();