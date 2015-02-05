using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

	public int maxHealth;
	public int curHealth;
	public float rotationOffset;
	public int size;
	public List<Targeter> watchers;

	public List<GameObject> weapons;
	public GameObject engine;

	private List<GameObject>[] weaponResolutions = new List<GameObject>[4];

	public interface Targeter {
		void handleTargetDeath();
	}

	// Use this for initialization
	void Start () {
		watchers = new List<Targeter> ();
		weapons = new List<GameObject> ();
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

	public void moveToCoords(Vector3 mousePos) {
		engine.GetComponent<Thruster> ().startMove (mousePos, rotationOffset);
	}

	public void looktAt(Vector3 location) {
		engine.GetComponent<Thruster> ().startLook (location, rotationOffset);
	}
}
