Station.ResourceMgr = {};

Station.ResourceMgr.resourceMgr = function() {
	this.available = new Station.Resources.bundle();
	this.nextRound = new Station.Resources.bundle();
};

Station.ResourceMgr.processRound = function(_resourceMgr) {
	Station.Resources.clearConstantResources(_resourceMgr.available);
	Station.Resources.adjustResourceBundle(_resourceMgr.available, _resourceMgr.nextRound, true);
	Station.Resources.clearBundle(_resourceMgr.nextRound);
};

Station.ResourceMgr.getResourceCount = function(_resourceMgr) {
	var _outArr = [];
	for (var i=0 ; i<_resourceMgr.available.resources.length ; i++) {
		_outArr.push([Station.Resources.getModuleFromID(i).name, _resourceMgr.available.resources[i]]);
	}
	
	return _outArr;
};

// should really be used for debugging
Station.ResourceMgr.adjustResources = function(_resourceMgr, _bundle, _add) {
	Station.Resources.adjustResourceBundle(_resourceMgr.available, _bundle, _add);
};

Station.ResourceMgr.canAddResourceBundle = function(_target, _source) {
	return true;
};

Station.ResourceMgr.canRemoveResourceBundle = function(_target, _source) {
	for (var i=0 ; i<source.resources.length ; i++) {
		if (_target.resources[i] - _source.resources[i] < 0) {
			return false;
		}
	}
	return true;
};

