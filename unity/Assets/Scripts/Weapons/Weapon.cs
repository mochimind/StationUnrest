using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour, Targeter, FiringArcHandler.TargetingArcListener {

	public GameObject target; 

	protected bool canFire = false;
	protected bool allowFire = true;

	public int damage;
	public float firingDelay;
	public float firingTime;
	public int pulseCount;
	public float pulseDelay;
	public float maxRange;
	public float gunRotation;
	public int resolution;

	protected FiringState state = FiringState.Stopped;
	protected int pulses;
	protected float nextStateCountdown = 0f;
	protected bool hasFiringSolution = false;
	protected List<string> firingArcs = new List<string>();


	protected enum FiringState {
		GunCooldown, PulseCooldown, Pulsing, Ready, Stopped
	}

	// Use this for initialization
	void Start () {

	}

	public void setFiringArc(bool top, bool bot, bool left, bool right) {
		transform.FindChild (FiringArc.TOP).gameObject.SetActive (top);
		transform.FindChild (FiringArc.BOTTOM).gameObject.SetActive (bot);
		transform.FindChild (FiringArc.LEFT).gameObject.SetActive (left);
		transform.FindChild (FiringArc.RIGHT).gameObject.SetActive (right);
		if (top) {firingArcs.Add(FiringArc.TOP); }
		if (bot) {firingArcs.Add(FiringArc.BOTTOM); }
		if (left) {firingArcs.Add(FiringArc.LEFT); }
		if (right) {firingArcs.Add(FiringArc.RIGHT); }
	}
	
	// Update is called once per frame
	void Update () {
		if (state != FiringState.Stopped) {
			processFire();
		}
	}
	
	void Targeter.handleTargetDeath() {
		ceaseFire ();
	}
	
	void Targeter.handleTargetMove(Vector3 _target) {}

	void FiringArcHandler.TargetingArcListener.targetEntered () { 
		if (state == FiringState.Stopped) {
			state = FiringState.Ready;
		}
	}

	void FiringArcHandler.TargetingArcListener.targetExited() { ceaseFire(); }

	public void setTarget(GameObject _target) {
		if (target != null) {
			target.GetComponent<Ship> ().handleStopTarget (this);
		}
		target = _target;
		target.GetComponent<Ship> ().handleTarget (this);
		foreach (string token in firingArcs) {
			transform.FindChild(token).GetComponent<FiringArcHandler>().setTarget(target, this);
		}
	}

	public virtual void ceaseFire () { state = FiringState.Stopped; }

	protected virtual void processFire() {
		nextStateCountdown -= Time.deltaTime ;
		if (state == Weapon.FiringState.Ready || (state == Weapon.FiringState.GunCooldown && nextStateCountdown <= 0f)) {
			nextStateCountdown = firingTime;
			pulses = pulseCount;
			state = Weapon.FiringState.Pulsing;
			pulses --;
			startFire ();
			
		} else if (state == Weapon.FiringState.Pulsing) {
			updateFire();
			if (nextStateCountdown <= 0f) {
				completeFire();
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
				startFire ();
			}
		}
	}

	protected virtual void startFire() {}
	protected virtual void updateFire () {}
	protected virtual void completeFire() {}

	protected void setFiringArcVisibility(string arc, bool visible) {
		transform.FindChild(arc).gameObject.GetComponent<SpriteRenderer> ().enabled = visible;
	}

	public void displayFiringArcs() {
		foreach (string token in firingArcs) {
			transform.FindChild(token).GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	public void hideFiringArcs() {
		foreach (string token in firingArcs) {
			transform.FindChild(token).GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}

public class FiringArc {
	public static readonly string TOP = "FiringArcTop";
	public static readonly string BOTTOM = "FiringArcBottom";
	public static readonly string LEFT = "FiringArcLeft";
	public static readonly string RIGHT = "FiringArcRight";
}
