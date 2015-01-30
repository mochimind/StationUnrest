Station.Game = {};

Station.Game.init = function() {
	var _addShip = new Station.Ship.ship("Enterprise");
	Station.ShipMgr.addShip(_addShip);
	
	var _addMods = [];
	_addMods.push([Station.Modules.PowerStation.id, 1]);
	_addMods.push([Station.Modules.CrewBarracks.id, 1]);
	_addMods.push([Station.Modules.OrganicsProcessor.id, 1]);
	_addMods.push([Station.Modules.Refinery.id, 1]);
	
	Station.Modules.adjustModules(_addShip.modules, _addMods);
};

Station.Game.processRound = function() {
	console.log('hit');
};