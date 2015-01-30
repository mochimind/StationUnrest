var Station = {};
Station.Main = {};

Station.ERROR = -333;
Station.OK = 333;

window.onload = function() {
	Station.ShipMgr.init();

	Station.Game.init();
	
	Station.Interface.init();
	
	window.setInterval(Station.Game.processRound, 1000);
};

