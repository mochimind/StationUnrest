using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : Targetable {
	public float rotationOffset;
	public int size;

	public List<GameObject> weapons = new List<GameObject> ();
	public GameObject engine;

	private List<GameObject>[] weaponResolutions = new List<GameObject>[4];

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void handleClick() {
		foreach (GameObject token in weapons) {
			token.GetComponent<Weapon>().displayFiringArcs();
		}
	}

	public void handleUnclick() {
		foreach (GameObject token in weapons) {
			token.GetComponent<Weapon>().hideFiringArcs();
		}
	}

	public void moveToCoords(Vector3 mousePos) {
		engine.GetComponent<Thruster> ().startMove (mousePos, rotationOffset);
		foreach (Targeter t in watchers) {
			t.handleTargetMove(mousePos);
		}
	}

	public void maintainDistance(Vector3 _location, float _dist) {
		engine.GetComponent<Thruster> ().keepDistance (_location, rotationOffset, _dist);
		foreach (Targeter t in watchers) {
			t.handleTargetMove(_location);
		}
	}

	public void looktAt(Vector3 location) {
		engine.GetComponent<Thruster> ().startLook (location, rotationOffset);
	}
}
