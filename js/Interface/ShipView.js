Station.ShipView = {};

Station.ShipView.init = function(_ship) {
	Station.ShipView.ship = _ship;
	Station.Interface.unloadView();
	Station.TableViewer.initDisplay(["Modules Built"]);

	Station.ShipView.modules = [];
	var _tempArray;
	for (var i=0 ; i<_ship.modules.modCount.length ; i++) {
		_tempArray = [];
		_tempArray.push(Station.Modules.getModuleFromID(i).name);
		_tempArray.push(_ship.modules.modCount[i]);
		_tempArray.push("+");
		_tempArray.push("-");

		// now we want to add the buttons to build more modules
		_tempArray = Station.TableViewer.createEntry(_tempArray);
		Station.ShipView.modules.push(_tempArray);
		(function(_moduleMgr, _module) {
			_tempArray[3].addEventListener("click", function() {
				Station.Modules.addModule(_ship, _module, 1);
			});			
			_tempArray[4].addEventListener("click", function() {
				Station.Modules.removeModule(_ship, _module, 1);
			});			
		}) (Station.ShipMgr.shipList[i], i);
	}
	
	Station.TableViewer.createHeader(["Resources Available"]);
	Station.ShipView.resources = [];
	for (var i=0 ; i<_ship.resourceMgr.available.resources.length ; i++) {
		_tempArray = [];
		_tempArray.push(Station.Resources.getModuleFromID(i).name);
		_tempArray.push(_ship.resourceMgr.available.resources[i]);
		Station.ShipView.resources.push(Station.TableViewer.createEntry(_tempArray));
	}
	
	// power
	_tempArray = [];
	_tempArray.push(Station.Resources.Power.name);
	_tempArray.push(_ship.resourceMgr.powerAvailable + "/" + (_ship.resourceMgr.powerUsed + _ship.resourceMgr.powerAvailable));
	Station.ShipView.resources.push(Station.TableViewer.createEntry(_tempArray));
	
	// housing
	_tempArray = [];
	_tempArray.push(Station.Resources.Housing.name);
	_tempArray.push(_ship.resourceMgr.housingAvailable + "/" + (_ship.resourceMgr.housingUsed + _ship.resourceMgr.housingAvailable));	
	Station.ShipView.resources.push(Station.TableViewer.createEntry(_tempArray));
	
	Station.Interface.loadView(Station.ShipView);
};

Station.ShipView.updateView = function() {
	for (var i=0 ; i<Station.ShipView.ship.modules.modCount.length ; i++) {
		Station.TableViewer.modifyEntry(Station.ShipView.modules[i], 1, Station.ShipView.ship.modules.modCount[i]);
	}

	var i;
	for (i=0 ; i<Station.ShipView.ship.resourceMgr.available.resources.length ; i++) {
		Station.TableViewer.modifyEntry(Station.ShipView.resources[i], 1, Station.ShipView.ship.resourceMgr.available.resources[i]);
	}
	
	// power
	Station.TableViewer.modifyEntry(Station.ShipView.resources[i], 1, Station.ShipView.ship.resourceMgr.powerAvailable + "/" + 
			(Station.ShipView.ship.resourceMgr.powerUsed + Station.ShipView.ship.resourceMgr.powerAvailable));
	i++;
	
	// housing
	Station.TableViewer.modifyEntry(Station.ShipView.resources[i], 1, Station.ShipView.ship.resourceMgr.housingAvailable + "/" + 
			(Station.ShipView.ship.resourceMgr.housingUsed + Station.ShipView.ship.resourceMgr.housingAvailable));
};

Station.ShipView.unloadView = function() {
	Station.TableViewer.unloadView();
};