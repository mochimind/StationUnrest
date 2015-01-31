Station.Game = {};

Station.Game.init = function() {
	var _addShip = new Station.Ship.ship("Enterprise");
	Station.ShipMgr.addShip(_addShip);
	
	var _addObj = [];
	_addObj.push([Station.Modules.PowerStation.id, 1]);
	_addObj.push([Station.Modules.CrewBarracks.id, 1]);
	_addObj.push([Station.Modules.OrganicsProcessor.id, 1]);
	_addObj.push([Station.Modules.Refinery.id, 1]);
	Station.Modules.adjustModules(_addShip.modules, _addObj);
	
	_addObj = new Station.Resources.bundle();
	Station.Resources.adjustResource(_addObj, Station.Resources.Polymer.id, 101, true);
	Station.Resources.adjustResource(_addObj, Station.Resources.Fuel.id, 102, true);
	Station.Resources.adjustResource(_addObj, Station.Resources.Gas.id, 104, true);
	Station.ResourceMgr.adjustResources(_addShip.resourceMgr, _addObj, true);
};

Station.Game.processRound = function() {
	console.log('hit');
	Station.Interface.updateInterface();
};