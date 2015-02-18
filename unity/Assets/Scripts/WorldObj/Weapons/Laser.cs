using UnityEngine;
using System.Collections.Generic;

public class Laser : Weapon{

	private LineRenderer laser;

	protected override void startFire() {
		laser = gameObject.AddComponent<LineRenderer> ();
		laser.material = new Material (Shader.Find ("Particles/Additive"));
		laser.SetColors (Color.red, Color.red);
		laser.SetWidth (0.1f, 0.1f);
		laser.SetVertexCount (2);
		target.GetComponent<Ship> ().handleDamage (damage);
	}

	protected override void updateFire () {
		laser.SetPosition (0, transform.position);
		laser.SetPosition (1, target.transform.position);
	}

	protected override void completeFire () {
		Destroy (laser);
	}

	public override void ceaseFire () {
		if (state == Weapon.FiringState.Pulsing) {
			Destroy (laser);
		}
		base.ceaseFire ();
	}
}
