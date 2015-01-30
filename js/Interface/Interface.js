Station.Interface = {};

Station.Interface.init = function () {
	Station.ShipList.init();
};

Station.Interface.unloadView = function() {
	if (Station.Interface.curView != null) {
		console.log("cur view: " + Station.Interface.curView);
		document.body.removeChild(Station.Interface.curView);
		Station.Interface.curView = null;
	}
};

Station.Interface.loadView = function(_addObj) {
	Station.Interface.unloadView();
	Station.Interface.curView = _addObj;
};