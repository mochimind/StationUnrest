Station.Ship = {};

Station.Ship.ship = function(_name) {
	this.name = _name;
	this.modules = new Station.Modules.modules();
	this.resourceMgr = new Station.ResourceMgr.resourceMgr();
};

