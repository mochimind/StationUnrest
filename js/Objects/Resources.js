Station.Resources = {};

Station.Resources.defaultResources = function() {
	return [0,0,0,0,0,0];
};

Station.Resources.init = function() {
	Station.Resources.Collection = Station.Resources.defaultResources;
	Station.Resources.Collection[Station.Resources.Power.id] = Station.Resources.Power;
	Station.Resources.Collection[Station.Resources.Polymer.id] = Station.Resources.Polymer;
	Station.Resources.Collection[Station.Resources.Fuel.id] = Station.Resources.Fuel;
	Station.Resources.Collection[Station.Resources.Housing.id] = Station.Resources.Housing;
	Station.Resources.Collection[Station.Resources.Gas.id] = Station.Resources.Gas;
	Station.Resources.Collection[Station.Resources.Effort.id] = Station.Resources.Effort;
};

Station.Resources.bundle = function() {
	this.resources = Station.Resources.defaultResources();
};

Station.Resources.resourceBundleFromArray = function(_data) {
	var _outBundle = new Station.Resources.bundle();
	
	for (var i=0 ; i<_data.length ; i++) {
		_outBundle.resources[_data[i][0]] += _data[i][1];
	}
	return _outBundle;
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
	return Station.Resources.Collection[_id];
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
Station.Resources.Effort = {
		id: 5,
		name: 'effort',
		accumulate: false
}