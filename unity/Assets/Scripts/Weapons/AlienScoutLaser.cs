using UnityEngine;
using System.Collections;

public class AlienScoutLaser : Weapon{
	public int damage;
	public float firingDelay;
	public float firingTime;
	public int pulseCount;
	public float pulseDelay;
	public float firingArc;	// degrees
	public float maxRange;
	public float gunRotation;

	private curState state = curState.Ready;
	private int pulses;
	private float nextStateCountdown = 0f;

	private LineRenderer laser;

	private enum curState {
		GunCooldown, PulseCooldown, Pulsing, Ready, Stopped
	}

	public override void processFire () {
		nextStateCountdown -= Time.deltaTime ;
		if (state == curState.Ready || (state == curState.GunCooldown && nextStateCountdown <= 0f)) {
			// if there's a shot, take it
			if (inFiringSolution()) {
				nextStateCountdown = firingTime;
				pulses = pulseCount;
				state = curState.Pulsing;
				pulses --;
				fire ();
			}
			// wait for the shot to clear
		} else if (state == curState.Pulsing) {
			laser.SetPosition (0, transform.parent.position);
			laser.SetPosition (1, target.transform.position);
			if (nextStateCountdown <= 0f) {
				Destroy (laser);
				if (pulses > 0) {
					state = curState.PulseCooldown;
					nextStateCountdown = pulseDelay;
				} else {
					state = curState.GunCooldown;
					nextStateCountdown = firingDelay;
				}
			}
		} else if (state == curState.PulseCooldown) {
			if (nextStateCountdown <= 0f) {
				state = curState.Pulsing;
				pulses --;
				nextStateCountdown = firingTime;
				fire ();
			}
		}
	}

	private bool inFiringSolution() {
		// check if in firing arc
		float offsetAngle = gunRotation + transform.parent.gameObject.GetComponent<Ship> ().rotationOffset;
		if (Mathf.Abs(Angle.RotationAngle (transform.parent, target.transform.position, offsetAngle)) > firingArc) {
			return false;
		}

		// check if in firing range
		float curDist = Vector2.Distance (new Vector2(transform.parent.position.x, transform.parent.position.y), 
		                                  new Vector2(target.transform.position.x, target.transform.position.y));
		if (curDist > maxRange) {
			return false;
		} else {
			return true;
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
		if (state == curState.Pulsing) {
			Destroy (laser);
		}
		state = curState.Stopped;
	}
}
