using UnityEngine;
using System.Collections;

public class Launcher : Weapon {
	public GameObject ammo;
	
	protected override void startFire() {
		GameObject shot = (GameObject) Instantiate (ammo);
		shot.GetComponent<Missile>().initialize(transform.parent.gameObject);
		shot.GetComponent<MissileAI> ().setTarget(target);
	}
}
