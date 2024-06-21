
var myApp = myApp || {};

(function () {
	myApp.utils = {
		delay : function(ms) {
			return new Promise(resolve => setTimeout(resolve, ms));
		}
	};
})();