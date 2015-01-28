Station.TableViewer = {};

// this class builds a table to display content

// ensure the display is initialized first
Station.TableViewer.initDisplay = function(_data) {
	console.log('here');
	Station.TableViewer.columns = _data.length;
	Station.TableViewer.tableDiv = document.createElement('table');
	Station.TableViewer.tableDiv.class = "main";
	Station.TableViewer.tableDiv.id = "main_list";
	document.body.appendChild(Station.TableViewer.tableDiv);
	
	var _tempRow = document.createElement('th');
	for (var i=0 ; i<_data.length ; i++) {
		var _tempCol = document.createElement('td');
		_tempCol.innerHTML = _data[i];
		_tempRow.appendChild(_tempCol);
	}
	Station.TableViewer.tableDiv.appendChild(_tempRow);
	Station.Main.loadView(Station.TableViewer.tableDiv);
};

Station.TableViewer.createEntry = function(_data) {
	console.log('alive');
	if (_data.length != this.columns) {
		console.log("Error: columns count does not match in Table!" + _data.length + "," + this.columns);
		return Station.ERROR;
	}
	
	var _tempRow = document.createElement('tr');
	for (var i=0 ; i<_data.length ; i++) {
		var _tempCol = document.createElement('td');
		_tempCol.innerHTML = _data[i];
		_tempRow.appendChild(_tempCol);
	}
	Station.TableViewer.tableDiv.appendChild(_tempRow);
	
	return _tempRow;
};

// tear down the display once we are done
Station.TableViewer.destroyDisplay = function() {
	document.body.removeChild(Station.TableViewer.tableDiv);
	Station.TableViewer.tableDiv = null;
	Station.TableViewer.columns = 0;
};
