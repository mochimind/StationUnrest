using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

	public int maxHealth;
	public List<Targeter> watchers;

	public interface Targeter {
		void handleDeath();
	}

	private int curHealth = -1;

	// Use this for initialization
	void Start () {
		watchers = new List<Targeter> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (curHealth == -1) {
			curHealth = maxHealth;
		}
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
				t.handleDeath();
			}
			Destroy (this.gameObject);
		}
	}
}
