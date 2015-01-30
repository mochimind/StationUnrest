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
Station.Modules.adjustModules = function(_module, _gifts) {
	for (var j=0 ; j<_gifts.length ; j++) {
		_module.modCount[_gifts[j][0]] += _gifts[j][1];
		var _modSpace = Station.Modules.getModuleFromID(_gifts[j][0]).space;
		_module.availableSpace -= _gifts[j][1] * _modSpace;
		_module.usedSpace += _gifts[j][1] * _modSpace;
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
	
	// consume the resources
	Station.ResourceMgr.adjustResources(_ship.resourceMgr, _tempResource, false);
	
	// consume the space
	_ship.resourceMgr.availableSpace -= _tempMod.space * _count;
	_ship.resourceMgr.usedSpace += _tempMod.space * _count;
	
	// reward the modules
	_ship.resourceMgr[_module] += _count;
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
			buildCost: Station.Resources.resourceBundleFromArray([[Station.Resources.Polymer.id, 9]]),
			effort: 8,
			space: 1,
			operatingCost: [['fuel', 1], ['labor', 1]],
			produces: [['power', 4]],
			description: "One of the first reactor designs popularized in the colonization age. Really the only thing good about this reactor is it's cheap and easy to make."
	};

	Station.Modules.CrewBarracks = {
			id: 1,
			name: "Crew Barracks",
			buildCost: [['polymer', 32],['effort', 24]],
			space: 4,
			operatingCost: [['power', 2]],		
			produces: [['housing', 40]],
			description: ""
	};
	Station.Modules.OrganicsProcessor = {
			id: 2,
			name: "Organics Processor",
			buildCost: [['polymer', 5], ['effort', 5]],
			space: 1,
			operatingCost: [['labor', 1], ['power', 1]],		
			produces: [['food', 6]]
	};
	Station.Modules.Refinery = {
			id: 3,
			name: "Refinery",
			buildCost: [['polymer', 16], ['effort', 12]],
			space: 2,
			operatingCost: [['power', 3], ['labor', 2], ['gas', 1]],
			produces: [['fuel', 2]]
			
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
