Station.ShipView = {};

Station.ShipView.init = function(_ship) {
	console.log("ship is: " + _ship);
	this.ship = _ship;
	Station.Interface.unloadView();
	Station.TableViewer.initDisplay(["Modules"]);

	var _tempModules = Station.Modules.getModulesCount(_ship.modules);
	Station.ShipView.modules = [];
	for (var i=0 ; i<_tempModules.length ; i++) {
		Station.ShipView.modules.push(Station.TableViewer.createEntry(_tempModules[i]));
	}	
};