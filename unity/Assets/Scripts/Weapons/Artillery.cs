using UnityEngine;
using System.Collections;

public class Artillery : Weapon {
	public GameObject ammo;

	protected override void startFire() {
		GameObject shot = (GameObject) Instantiate (ammo);

	}
}
