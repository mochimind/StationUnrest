Station.ShipView = {};

Station.ShipView.init = function(_ship) {
	this.ship = _ship;
	Station.Interface.unloadView();
	Station.TableViewer.initDisplay(["Modules"]);

	var _tempArray = Station.Modules.getModulesCount(_ship.modules);
	Station.ShipView.modules = [];
	for (var i=0 ; i<_tempArray.length ; i++) {
		Station.ShipView.modules.push(Station.TableViewer.createEntry(_tempArray[i]));
	}
	
	Station.TableViewer.createHeader(["Resources"]);
	Station.ShipView.resources = [];
	_tempArray = Station.ResourceMgr.getResourceCount(_ship.resourceMgr);
	for (var i=0 ; i<_tempArray.length ; i++) {
		Station.ShipView.resources.push(Station.TableViewer.createEntry(_tempArray[i]));
	}
};