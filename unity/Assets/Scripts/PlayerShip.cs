using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject equip = (GameObject) Instantiate (Resources.Load ("ShipModules/Engines/CruiserDefaultThruster"));
		equip.transform.parent = transform;
		gameObject.GetComponent<Ship> ().engine = equip;

		equip = (GameObject)Instantiate (Resources.Load ("ShipModules/Weapons/AlienScoutLaser"));
		equip.transform.parent = transform;
		gameObject.GetComponent<Ship> ().weapons.Add (equip);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void clicked() {
		MainGUIMgr.SelectShip (gameObject);
		gameObject.GetComponent<Ship> ().handleClick ();
	}

	void unclicked() {
		gameObject.GetComponent<Ship> ().handleUnclick ();
		MainGUIMgr.SelectNothing ();
	}

	void offTargetClick(Vector3 mousePos) {
		gameObject.GetComponent<Ship> ().moveToCoords (mousePos);
	}
}
