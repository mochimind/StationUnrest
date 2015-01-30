Station.Resources = {};

Station.Resources.defaultResources = function() {
	return [0,0,0,0,0];
};
Station.Resources.bundle = function() {
	this.resources = Station.Resources.defaultResources();
};

Station.Resources.clearBundle = function(_bundle) {
	_bundle.resources = Station.Resources.defaultResources();
};

Station.Resources.adjustResource = function(_target, _type, _amount, _add) {
	if (_add) {
		_target.resources[_type] += _amount;
	} else {
		_target.resources[_type] -= _amount;				
	}
};

Station.Resources.adjustResourceBundle = function(_target, _source, _add) {
	for (var i=0 ; i<_source.resources.length ; i++) {
		if (_add) {
			_target.resources[i] += _source.resources[i];			
		} else {
			_target.resources[i] -= _source.resources[i];						
		}
	}
};

Station.Resources.getModuleFromID = function(_id) {
	switch(_id) {
	case Station.Resources.Power.id:
		return Station.Resources.Power;
	case Station.Resources.Polymer.id:
		return Station.Resources.Polymer;
	case Station.Resources.Fuel.id:
		return Station.Resources.Fuel;
	case Station.Resources.Housing.id:
		return Station.Resources.Housing;
	case Station.Resources.Gas.id:
		return Station.Resources.Gas;
	}
};

// this is inefficient, but is scalable when more resources are added
Station.Resources.clearConstantResources = function(_bundle) {
	for (var i=0 ; i<_bundle.resources.length ; i++) {
		if (!Station.Resources.getModuleFromID(i).accumulate) {
			_bundle[i] = 0;
		}
	}
};

Station.Resources.Power = {
		id: 0,
		name: 'power',
		accumulate: false
};
Station.Resources.Polymer = {
		id: 1,
		name: 'polymer',
		accumulate: true
};
Station.Resources.Fuel = {
		id: 2,
		name: 'fuel',
		accumulate: true
};
Station.Resources.Housing = {
		id: 3,
		name: 'housing',
		accumulate: false
};
Station.Resources.Gas = {
		id: 4,
		name: 'gas',
		accumulate: true
};