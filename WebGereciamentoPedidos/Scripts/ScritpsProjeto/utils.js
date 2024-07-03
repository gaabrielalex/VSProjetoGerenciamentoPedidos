
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
		converterParaStringEmFormatoBrUmFLoat: function (numero) {
			return numero.toLocaleString('pt-BR', { minimumFractionDigits: 2 });
		},
		aCadaTresCasasInteirasAdicionarPonto: function (numero) {
			let numeroString = numero.toString();
			let numeroStringComPonto = '';
			let contador = 0;

			for (let i = numeroString.length - 1; i >= 0; i--) {
				numeroStringComPonto = numeroString[i] + numeroStringComPonto;
				contador++;

				if (contador === 3) {
					numeroStringComPonto = '.' + numeroStringComPonto;
					contador = 0;
				}
			}

			return numeroStringComPonto;
		}
	};
})();