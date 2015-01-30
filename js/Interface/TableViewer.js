Station.TableViewer = {};

// this class builds a table to display content

// ensure the display is initialized first
Station.TableViewer.initDisplay = function(_data) {
	Station.TableViewer.columns = _data.length;
	Station.TableViewer.tableDiv = document.createElement('table');
	Station.TableViewer.tableDiv.class = "main";
	Station.TableViewer.tableDiv.id = "main_list";
	document.body.appendChild(Station.TableViewer.tableDiv);
	
	Station.TableViewer.createHeader(_data);
};

Station.TableViewer.createHeader = function(_data) {
	var _outObjs = [];
	var _tempRow = document.createElement('tr');
	_outObjs.push(_tempRow);
	for (var i=0 ; i<_data.length ; i++) {
		var _tempCol = document.createElement('th');
		_tempCol.innerHTML = _data[i];
		_tempRow.appendChild(_tempCol);
		_outObjs.push(_tempCol);
	}	
	
	Station.TableViewer.tableDiv.appendChild(_tempRow);
	return _outObjs;
};

Station.TableViewer.createEntry = function(_data) {
	//if (_data.length != this.columns) {
	//	console.log("Error: columns count does not match in Table!" + _data.length + "," + this.columns);
	//	return Station.ERROR;
	//}
	
	var _outObjs = [];
	var _tempRow = document.createElement('tr');
	_outObjs.push(_tempRow);
	for (var i=0 ; i<_data.length ; i++) {
		var _tempCol = document.createElement('td');
		_tempCol.innerHTML = _data[i];
		_tempRow.appendChild(_tempCol);
		_outObjs.push(_tempCol);
	}
	Station.TableViewer.tableDiv.appendChild(_tempRow);
	
	return _outObjs;
};

Station.TableViewer.modifyEntry = function(_entry, _index, _value) {
	// it's index + 1 because the first entry is the tr
	_entry[_index+1].innerHTML = _value;
};

// tear down the display once we are done
Station.TableViewer.unloadView = function() {
	document.body.removeChild(Station.TableViewer.tableDiv);
	Station.TableViewer.tableDiv = null;
	Station.TableViewer.columns = 0;
};
