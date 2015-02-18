using UnityEngine;
using System.Collections;

public class Missile : Destructable {
	public int damage;
	private GameObject parent;

	public void initialize(GameObject _parent) { 
		parent = _parent;
		transform.position = _parent.transform.position;
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Destructable d = collision.gameObject.GetComponent<Destructable> ();
		if (d!= null && collision.gameObject != parent) {
			d.handleDamage(damage);
			onDeath();
		}
	}
}
