using UnityEngine;
using System.Collections.Generic;

public class Destructable : MonoBehaviour {
	public int maxHealth;
	public int curHealth;

	public float getPercentHealth() {
		return (float)curHealth / maxHealth;
	}
	
	public void handleDamage(int damage) {
		curHealth -= damage;
		if (curHealth <= 0) {
			onDeath();
		}
	}

	public virtual void onDeath() {
		Destroy (this.gameObject);
	}
}
