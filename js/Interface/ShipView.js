Station.ShipView = {};

Station.ShipView.init = function(_ship) {
	this.ship = _ship;
	Station.Interface.unloadView();
	Station.TableViewer.initDisplay(["Modules Built"]);

	Station.ShipView.modules = [];
	for (var i=0 ; i<_ship.modules.modCount.length ; i++) {
		var _tempArray = [];
		_tempArray.push(Station.Modules.getModuleFromID(i).name);
		_tempArray.push(_ship.modules.modCount[i]);
		_tempArray.push("+");
		_tempArray.push("-");

		// now we want to add the buttons to build more modules
		_tempArray = Station.TableViewer.createEntry(_tempArray)
		Station.ShipView.modules.push(_tempArray);
		(function(_moduleMgr, _module) {
			_tempArray[3].addEventListener("click", function() {
				Station.Modules.addModule(_ship.modules, i, 1);
			});			
			_tempArray[4].addEventListener("click", function() {
				Station.Modules.removeModule(_ship.modules, i, 1);
			});			
		}) (Station.ShipMgr.shipList[i]);
	}
	
	Station.TableViewer.createHeader(["Resources Available"]);
	Station.ShipView.resources = [];
	_tempArray = Station.ResourceMgr.getResourceCount(_ship.resourceMgr);
	for (var i=0 ; i<_ship.resourceMgr.available.resources.length ; i++) {
		var _tempArray = [];
		_tempArray.push(Station.Resources.getModuleFromID(i).name);
		_tempArray.push(_ship.resourceMgr.available.resources[i]);
		Station.ShipView.resources.push(Station.TableViewer.createEntry(_tempArray));
	}
};