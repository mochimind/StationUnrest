using UnityEngine;
using System.Collections;

public class AlienScout : MonoBehaviour, Ship.Targeter {

	private GameObject target = null;
	public float rotationOffset;

	private LineRenderer laser;

	// Use this for initialization
	void Start () {
		// give the scout a weapon
		GameObject equip = (GameObject) Instantiate (Resources.Load("ShipModules/Weapons/AlienScoutLaser"));
		equip.transform.position = transform.position;
		equip.transform.parent = transform;
		gameObject.GetComponent<Ship> ().weapons.Add (equip);

		equip = (GameObject) Instantiate (Resources.Load ("ShipModules/Engines/AlienLightThruster"));
		equip.transform.position = transform.position;
		equip.transform.parent = transform;
		gameObject.GetComponent<Ship> ().engine = equip;

		// determine the nearest player ship
		target = PlayerShipMgr.GetNearestShip (transform.position.x, transform.position.y);
		foreach (GameObject weaponObj in gameObject.GetComponent<Ship>().weapons) {
			Weapon weapon = weaponObj.GetComponent<Weapon>();
			weapon.setTarget(target);
			Debug.Log ("implement range checking");

			weapon.startFire();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			Debug.Log ("fix this!");
			gameObject.GetComponent<Ship>().looktAt(target.transform.position);
		}
	}

	void Ship.Targeter.handleTargetDeath() {
		target = null;
		Debug.Log ("handle switching targets from alien scout");
	}

	void Ship.Targeter.handleTargetMove(Vector3 _location) {
	}

	private void faceTarget() {
		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

}
