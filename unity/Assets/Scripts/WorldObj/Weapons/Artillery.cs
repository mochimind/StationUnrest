using UnityEngine;
using System.Collections;

public class Artillery : Weapon {
	public GameObject ammo;

	protected override void startFire() {
		GameObject shot = (GameObject) Instantiate (ammo);
		shot.GetComponent<Projectile> ().setTarget (target, transform.parent.gameObject);
	}
}
