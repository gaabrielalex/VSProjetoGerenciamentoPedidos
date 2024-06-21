
var myApp = myApp || {};

(function () {
	myApp.showToast = function(message, type) {
		var toastElement = document.getElementById('toastMessage');
		var toastHeader = document.querySelector('#toastMessage .toast-header strong');
		var toastBody = document.querySelector('#toastMessage .toast-body');

		toastBody.innerHTML = message;

		if (type === 's') {
			toastHeader.innerHTML = 'Sucesso';
			toastElement.className = 'toast bg-success text-white';
		} else if (type === 'e') {
			toastHeader.innerHTML = 'Erro';
			toastElement.className = 'toast bg-danger text-white';
		}

		var toast = new bootstrap.Toast(toastElement);
		toast.show();
	}
})()