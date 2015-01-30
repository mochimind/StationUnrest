Station.Game = {};

Station.Game.init = function() {
	var _addShip = new Station.Ship.ship("Enterprise");
	Station.ShipMgr.addShip(_addShip);
	
	var _addMods = [];
	_addMods.push([Station.Modules.PowerStation.id, 1]);
	_addMods.push([Station.Modules.]);
	
	Station.Modules.adjustModules(_addShip.modules, _addMods);
};