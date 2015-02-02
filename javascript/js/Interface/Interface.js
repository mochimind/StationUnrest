Station.Interface = {};

Station.Interface.init = function () {
	Station.ShipList.init();
};

Station.Interface.unloadView = function() {
	if (Station.Interface.curView != null) {
		Station.Interface.curView.unloadView();
		Station.Interface.curView = null;
	}
};

Station.Interface.loadView = function(_addObj) {
	Station.Interface.unloadView();
	Station.Interface.curView = _addObj;
};

Station.Interface.updateInterface = function() {
	Station.Interface.curView.updateView();
};