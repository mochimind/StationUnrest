Station.ShipMgr = {};

Station.ShipMgr.init = function() {
	Station.ShipMgr.shipList = [];
};

Station.ShipMgr.addShip = function(_ship) {
	Station.ShipMgr.shipList.push(_ship);
};
