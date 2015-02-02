Station.People = {};
Station.People.LaborRatio = 0.7;

Station.Labor.laborMgr = function() {
	this.curPop = 0;
	this.curLabor = 0;
	this.allocatedLabor = 0;
	this.allocation = Station.Modules.defaultModules();
};

// _count number of _modules are looking to allocate labor onboard the _laborMgr
Station.Labor.registerLabor = function(_laborMgr, _module, _count) {
	
};

