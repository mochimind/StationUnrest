using UnityEngine;
using System.Collections.Generic;

public class PlayerShip : MonoBehaviour {

	private List<GameObject>[] weaponGroup = new List<GameObject>[4];

	// Use this for initialization
	void Start () {
		GameObject equip = (GameObject) Instantiate (Resources.Load ("ShipModules/Engines/CruiserDefaultThruster"));
		equip.transform.parent = transform;
		gameObject.GetComponent<Ship> ().engine = equip;

		equip = (GameObject)Instantiate (Resources.Load ("ShipModules/Weapons/AlienScoutLaser"));
		equip.transform.parent = transform;
		gameObject.GetComponent<Ship> ().weapons.Add (equip);

		weaponGroup[0] = new List<GameObject>();
		weaponGroup[0].Add(equip);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void clicked() {
		MainGUIMgr.SelectShip (gameObject);
		gameObject.GetComponent<Ship> ().handleClick ();
		WeaponGroup.ShowWeaponGroups(weaponGroup[0] != null, weaponGroup[1] != null,
		                             weaponGroup[2] != null, weaponGroup[3] != null);
	}

	void unclicked() {
		gameObject.GetComponent<Ship> ().handleUnclick ();
		MainGUIMgr.SelectNothing ();
		WeaponGroup.HideWeaponGroups();
	}

	void offTargetClick(Vector3 mousePos) {
		gameObject.GetComponent<Ship> ().moveToCoords (mousePos);
	}
}
