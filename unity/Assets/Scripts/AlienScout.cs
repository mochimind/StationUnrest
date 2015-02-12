using UnityEngine;
using System.Collections;

public class AlienScout : Clickable {

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
	}
	
	// Update is called once per frame
	void Update () {

	}
}
