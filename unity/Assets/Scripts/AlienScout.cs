using UnityEngine;
using System.Collections;

public class AlienScout : MonoBehaviour {

	private GameObject target = null;
	public float rotationOffset;

	private LineRenderer laser;

	// Use this for initialization
	void Start () {
		// give the scout a weapon
		GameObject equip = (GameObject) Instantiate (Resources.Load("ShipModules/Weapons/AlienScoutLaser"));
		equip.transform.parent = transform;
		gameObject.GetComponent<Ship> ().weapons.Add (equip);

		// determine the nearest player ship
		target = PlayerShipMgr.GetNearestShip (transform.position.x, transform.position.y);
		foreach (GameObject weaponObj in gameObject.GetComponent<Ship>().weapons) {
			Weapon weapon = weaponObj.GetComponent<Weapon>();
			weapon.setTarget(target);
			Debug.Log ("implement range checking");

			weapon.startFire();
		}

		// rotate to face target
		faceTarget ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void faceTarget() {
		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

}
