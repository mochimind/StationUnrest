using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

	public int maxHealth;
	public int curHealth;
	public List<Targeter> watchers;

	private Vector3 destination;
	private bool moving = false;
	public float speed;

	public interface Targeter {
		void handleDeath();
	}

	// Use this for initialization
	void Start () {
		watchers = new List<Targeter> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			Debug.Log ("trying");
			transform.position = Vector2.MoveTowards(transform.position, destination, speed);
			if (transform.position == destination) {
				Debug.Log ("stopped moving");
				moving = false;
			}
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

	public void moveToCoords(Vector3 mousePos) {
		destination = new Vector2 (mousePos.x, mousePos.y);
		moving = true;
	}

}
