using UnityEngine;
using System.Collections.Generic;

public class PlayerShip : Clickable, WeaponGroupButton.WeaponGroupListener {

	private List<GameObject>[] weaponGroup = new List<GameObject>[4];
	private bool[] weaponGroupStatus = new bool[4] {false, false, false, false};

	// Use this for initialization
	void Start () {
		GameObject equip = (GameObject) Instantiate (Resources.Load ("ShipModules/Engines/CruiserDefaultThruster"));
		equip.transform.parent = transform;
		gameObject.GetComponent<Propelled> ().engine = equip;

		equip = (GameObject)Instantiate (Resources.Load ("ShipModules/Weapons/120mm Artillery"));
		equip.transform.parent = transform;
		equip.GetComponent<Weapon> ().setFiringArc (true, true, true, true);
		gameObject.GetComponent<Ship> ().weapons.Add (equip);

		weaponGroup[0] = new List<GameObject>();
		weaponGroup[0].Add(equip);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public override void clicked() {
		MainGUIMgr.SelectShip (gameObject);
		WeaponGroup.ShowWeaponGroups(weaponGroup[0] != null, weaponGroup[1] != null,
		                             weaponGroup[2] != null, weaponGroup[3] != null, this);
	}

	public override bool handleClick (GameObject newObj, Vector3 mousePos) {
		if (newObj == null) {
			// moving there
			gameObject.GetComponent<Propelled> ().moveToCoords (mousePos);
			return true;
		}

		if (newObj.tag == gameObject.tag) {
			// we're on the same side, which means just switch to that object
			return false;
		}
		for (int i=0 ; i<weaponGroupStatus.Length ; i++) {
			if (weaponGroupStatus[i]) {
				foreach (GameObject token in weaponGroup[i]) {
					token.GetComponent<Weapon>().setTarget(newObj);
					token.GetComponent<Weapon>().hideFiringArcs();
				}
			}
		}
		WeaponGroup.DeselectWeaponGroups();
		return true;
	}

	public override void loseClickFocus () {
		MainGUIMgr.SelectNothing ();
		WeaponGroup.HideWeaponGroups();
	}
	
	void WeaponGroupButton.WeaponGroupListener.WGActivated (int id) {
		weaponGroupStatus[id-1] = true;
		// the list shouldn't be null because we specifically told which weapon groups to disable
		foreach (GameObject token in weaponGroup[id-1]) {
			token.GetComponent<Weapon>().displayFiringArcs();
		}
	}

	void WeaponGroupButton.WeaponGroupListener.WGDeactivated (int id) {
		weaponGroupStatus[id-1] = false;
		foreach (GameObject token in weaponGroup[id-1]) {
			token.GetComponent<Weapon>().hideFiringArcs();
		}
	}
}
