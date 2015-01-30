Station.ShipList = {};

Station.ShipList.init = function() {
	Station.Interface.unloadView();
	Station.TableViewer.initDisplay(["Ship","People"]);
	for (var i=0 ; i<Station.ShipMgr.shipList.length ; i++) {
		var _tempEntry = Station.TableViewer.createEntry([Station.ShipMgr.shipList[i].name, 0]);
		(function(_ship) {
			_tempEntry.addEventListener("click", function() {
				Station.ShipView.init(_ship);
			});			
		}) (Station.ShipMgr.shipList[i]);
	}
};

Station.ShipList.ShipClickedHandler = function(_curShip) {
	Station.ShipView.init(_curShip);
};

