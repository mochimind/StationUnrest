using UnityEngine;
using System.Collections;

public class Artillery : Weapon {
	public GameObject ammo;

	protected override void startFire() {
		target.GetComponent<Ship> ().handleDamage (damage);
	}
}
