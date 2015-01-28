Station.ShipList = {};

Station.ShipList.init = function() {
	Station.Main.unloadView();
	Station.TableViewer.initDisplay(["Ship","People"]);
};

