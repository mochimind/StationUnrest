Station.Modules = {};

Station.Modules.modules = function() {
	this.modCount = [0,0,0,0];
	this.availableSpace = 100;
	this.usedSpace = 0;
};

Station.Modules.getModulesCount = function(_module) {
	console.log("modList is: " + _module.modCount);
	return [[Station.Modules.PowerStation.name, _module.modCount[Station.Modules.PowerStation.id]],
	        [Station.Modules.CrewBarracks.name, _module.modCount[Station.Modules.CrewBarracks.id]],
	        [Station.Modules.OrganicsProcessor.name, _module.modCount[Station.Modules.OrganicsProcessor.id]],
	        [Station.Modules.Refinery.name, _module.modCount[Station.Modules.Refinery.id]]];	
};

// module is the module you want to change, _gifts is an array of modules you want to add to the ship
// this function is very dumb, it won't do any error checking
Station.Modules.adjustModules = function(_module, _gifts) {
	for (var i=0 ; i<_gifts.length ; i++) {
		_module.modCount[_gifts[i][0]] += _gifts[i][1];
		var _modSpace = Station.Modules.getModuleFromID(_gifts[i][0]).space;
		_module.availableSpace -= _gifts[i][1] * _modSpace;
		_module.usedSpace += _gifts[i][1] * _modSpace;
	}
};

Station.Modules.getModuleFromID = function(_id) {
	switch(_id) {
	case Station.Modules.PowerStation.id:
		return Station.Modules.PowerStation;
	case Station.Modules.CrewBarracks.id:
		return Station.Modules.CrewBarracks;
	case Station.Modules.OrganicsProcessor.id:
		return Station.Modules.OrganicsProcessor;
	case Station.Modules.Refinery.id:
		return Station.Modules.Refinery;
	}
};

Station.Modules.PowerStation = {
		id: 0,
		name: "Power Station",
		buildCost: [['polymer', 9], ['effort', 8]],
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
