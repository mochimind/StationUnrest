Station.Ship = {};

Station.Ship.ship = function(_name) {
	this.name = _name;
	this.modules = new Station.Modules.modules();
};

Station.Ship.ship.prototype.create = function() {
	
};
