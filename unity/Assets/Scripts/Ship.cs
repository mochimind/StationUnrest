using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

	public int maxHealth;
	public int curHealth;
	public float rotationOffset;
	public int size;
	public List<Targeter> watchers  = new List<Targeter> ();

	public List<GameObject> weapons = new List<GameObject> ();
	public GameObject engine;

	private List<GameObject>[] weaponResolutions = new List<GameObject>[4];

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public float getPercentHealth() {
		return (float)curHealth / maxHealth;
	}

	public void handleTarget(Targeter t) {
		watchers.Add (t);
	}

	public void handleStopTarget(Targeter t) {
		watchers.Remove (t);
	}

	public void handleDamage(int damage) {
		curHealth -= damage;
		if (curHealth <= 0) {
			foreach (Targeter t in watchers) {
				t.handleTargetDeath();
			}
			Destroy (this.gameObject);
		}
	}

	public void handleClick() {
		Debug.Log ("clicky" + weapons.Count);
		foreach (GameObject token in weapons) {
			token.GetComponent<Weapon>().displayFiringArcs();
		}
	}

	public void handleUnclick() {
		Debug.Log ("hiding");
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

	public void looktAt(Vector3 location) {
		engine.GetComponent<Thruster> ().startLook (location, rotationOffset);
	}
}
