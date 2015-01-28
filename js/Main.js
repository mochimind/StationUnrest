var Station = {};
Station.Main = {};

Station.ERROR = -333;
Station.OK = 333;

window.onload = function() {
	Station.ShipList.init();
};

Station.Main.unloadView = function() {
	if (Station.Main.curView != null) {
		document.body.removeChild(Station.Main.curView);
		Station.Main.curView = null;
	}
};

Station.Main.loadView = function(_addObj) {
	Station.Main.unloadView();
	Station.Main.curView = _addObj;
};