using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void clicked() {
		GUIMgr.SelectShip (gameObject);
	}

	void offTargetClick(Vector3 mousePos) {
		gameObject.GetComponent<Ship> ().moveToCoords (mousePos);
	}
}
