using UnityEngine;
using System.Collections;

public class AlienScoutLaser : Weapon{

	private LineRenderer laser;

	public override void processFire () {
		nextStateCountdown -= Time.deltaTime ;
		if (state == Weapon.FiringState.Ready || (state == Weapon.FiringState.GunCooldown && nextStateCountdown <= 0f)) {
			// if there's a shot, take it
			if (hasFiringSolution) {
				nextStateCountdown = firingTime;
				pulses = pulseCount;
				state = Weapon.FiringState.Pulsing;
				pulses --;
				fire ();
			}
			// wait for the shot to clear
		} else if (state == Weapon.FiringState.Pulsing) {
			laser.SetPosition (0, transform.parent.position);
			laser.SetPosition (1, target.transform.position);
			if (nextStateCountdown <= 0f) {
				Destroy (laser);
				if (pulses > 0) {
					state = Weapon.FiringState.PulseCooldown;
					nextStateCountdown = pulseDelay;
				} else {
					state = Weapon.FiringState.GunCooldown;
					nextStateCountdown = firingDelay;
				}
			}
		} else if (state == Weapon.FiringState.PulseCooldown) {
			if (nextStateCountdown <= 0f) {
				state = Weapon.FiringState.Pulsing;
				pulses --;
				nextStateCountdown = firingTime;
				fire ();
			}
		}
	}
	
	private void fire() {
		laser = gameObject.AddComponent<LineRenderer> ();
		laser.material = new Material (Shader.Find ("Particles/Additive"));
		laser.SetColors (Color.red, Color.red);
		laser.SetWidth (0.1f, 0.1f);
		laser.SetVertexCount (2);
		target.GetComponent<Ship> ().handleDamage (damage);
	}

	public override void ceaseFire () {
		if (state == Weapon.FiringState.Pulsing) {
			Destroy (laser);
		}
		state = Weapon.FiringState.Stopped;
	}

	protected override void enableFiringArcs () {
		setFiringArc (FiringArc.Top, true);
	}

	public override void showFiringArcs (){
		setFiringArcVisibility (FiringArc.Top, true);
	}

	public override void hideFiringArcs () {
		setFiringArcVisibility (FiringArc.Top, false);
	}
}
