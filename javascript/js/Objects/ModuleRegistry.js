Station.Modules = {};

Station.Modules.defaultModules = function() {
	return [0,0,0,0];
};

Station.Modules.modules = function() {
	this.modCount = Station.Modules.defaultModules();
	this.availableSpace = 100;
	this.usedSpace = 0;
};


// module is the module you want to change, _gifts is an array of modules you want to add to the ship
// this function is very dumb, it won't do any error checking
Station.Modules.adjustModules = function(_ship, _gifts) {
	for (var j=0 ; j<_gifts.length ; j++) {
		_ship.modules.modCount[_gifts[j][0]] += _gifts[j][1];
		var _modSpace = Station.Modules.getModuleFromID(_gifts[j][0]).space;
		_ship.modules.availableSpace -= _gifts[j][1] * _modSpace;
		_ship.modules.usedSpace += _gifts[j][1] * _modSpace;
		
		// activate effects:
		Station.Modules.activateEffect(_ship, _gifts[j][0], _gifts[j][1]);
		console.log("todo: implement effect");
	}
};

// _mgr is the module manager, _modID is the id of the module that has the efect, _count is the number of times it's activated
Station.Modules.activateEffect = function(_ship, _modID, _count) {
	var _tempMod = Station.Modules.getModuleFromID(_modID);
	if (_tempMod.effect != undefined) {
		for (var i=0 ; i<_tempMod.effect.length ; i++) {
			if (_tempMod.effect[i][0] == Station.Resources.Power.id) {
				_ship.resourceMgr.powerAvailable += _tempMod.effect[i][1] * _count;
			} else if (_tempMod.effect[i][0] == Station.Resources.Housing.id) {
				_ship.resourceMgr.housingAvailable += _tempMod.effect[i][1] * _count;
			}
		}
	}	
};

Station.Modules.addModule = function(_ship, _module, _count) {
	// check that there's enough space
	var _tempMod = Station.Modules.getModuleFromID(_module);
	if (_ship.modules.availableSpace < _count * _tempMod.space) {
		console.log("not enough space");
		return false;
	}
	
	// check that there's enough resources
	var _tempResource = Station.Resources.Multiply(_tempMod.buildCost, _count);
	if (!Station.ResourceMgr.canRemoveResources(_ship.resourceMgr, _tempResource)) {
		console.log("not enough resources");
		return false;
	}
	
	console.log("todo: implement effort to build buildings");
	
	// consume the resources
	Station.ResourceMgr.adjustResources(_ship.resourceMgr, _tempResource, false);
	
	// consume the space
	_ship.resourceMgr.availableSpace -= _tempMod.space * _count;
	_ship.resourceMgr.usedSpace += _tempMod.space * _count;
	
	// reward the modules
	_ship.modules.modCount[_module] += _count;
	
	// check for effects
	Station.Modules.activateEffect(_ship, _module, _count);

	Station.Interface.updateInterface();
};

Station.Modules.removeModule = function(_moduleMgr, _modules, _count) {
	// check that there's enough available
	
	// TODO: refund resources
	console.log("removing");
};

Station.Modules.getModuleFromID = function(_id) {
	return Station.Modules.Collection[_id];
};

Station.Modules.init = function() {
	Station.Modules.PowerStation = {
			id: 0,
			name: "Power Station",
			buildCost: Station.Resources.resourceBundleFromArray([
                              [Station.Resources.Polymer.id, 9]]),
			effort: 8,
			space: 1,
			labor: 1,
			operatingCost: Station.Resources.resourceBundleFromArray([
                              [Station.Resources.Fuel.id, 1]]),
			effect: [[Station.Resources.Power.id, 4]],
			description: "One of the first reactor designs popularized in the colonization age. Really the only thing good about this reactor is it's cheap and easy to make."
	};

	Station.Modules.CrewBarracks = {
			id: 1,
			name: "Crew Barracks",
			buildCost: Station.Resources.resourceBundleFromArray([
                              [Station.Resources.Polymer.id, 32]]),
            effort: 24,
			space: 4,
			power: 2,
			effect: [[Station.Resources.Housing.id, 40]],
			description: ""
	};
	Station.Modules.OrganicsProcessor = {
			id: 2,
			name: "Organics Processor",
			buildCost: Station.Resources.resourceBundleFromArray([
                              [Station.Resources.Polymer.id, 5]]),
            effort: 5,
			space: 1,
			labor: 1,
			power: 1,
			produces: Station.Resources.resourceBundleFromArray([
                              [Station.Resources.Food.id, 6]])
	};
	Station.Modules.Refinery = {
			id: 3,
			name: "Refinery",
			buildCost: Station.Resources.resourceBundleFromArray([
                              [Station.Resources.Polymer.id, 16]]),
			effort: 12,
			space: 2,
			labor: 2,
			power: 3,
			operatingCost: Station.Resources.resourceBundleFromArray([
                              [Station.Resources.Gas.id, 1]]),
			produces: Station.Resources.resourceBundleFromArray([
                              [Station.Resources.Fuel.id, 2]])
			
	};

	Station.Modules.PolymerFactory = {
			
	};
	Station.Modules.ReadingRoom = {};
	Station.Modules.Workshop = {};
	Station.Modules.Collection = Station.Modules.defaultModules();
	Station.Modules.Collection[Station.Modules.PowerStation.id] = Station.Modules.PowerStation;	
	Station.Modules.Collection[Station.Modules.CrewBarracks.id] = Station.Modules.CrewBarracks;	
	Station.Modules.Collection[Station.Modules.OrganicsProcessor.id] = Station.Modules.OrganicsProcessor;	
	Station.Modules.Collection[Station.Modules.Refinery.id] = Station.Modules.Refinery;	
};
