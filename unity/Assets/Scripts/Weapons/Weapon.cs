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
	public float firingArc;	// degrees
	public float maxRange;
	public float gunRotation;
	public int resolution;

	protected FiringState state = FiringState.Stopped;
	protected int pulses;
	protected float nextStateCountdown = 0f;
	protected bool hasFiringSolution = false;


	protected enum FiringState {
		GunCooldown, PulseCooldown, Pulsing, Ready, Stopped
	}

	// Use this for initialization
	void Start () {
		List<string> enabled = getFiringArcs ();
		if (!enabled.Contains (FiringArc.TOP)) {
			transform.FindChild(FiringArc.TOP).gameObject.SetActive(false);
		}
		if (!enabled.Contains (FiringArc.BOTTOM)) {
			transform.FindChild(FiringArc.BOTTOM).gameObject.SetActive(false);
		}
		if (!enabled.Contains (FiringArc.LEFT)) {
			transform.FindChild(FiringArc.LEFT).gameObject.SetActive(false);
		}
		if (!enabled.Contains (FiringArc.RIGHT)) {
			transform.FindChild(FiringArc.RIGHT).gameObject.SetActive(false);
		}

		//hideFiringArcs ();
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
		foreach (string token in getFiringArcs()) {
			transform.FindChild(token).GetComponent<FiringArcHandler>().setTarget(target, this);
		}
	}

	public virtual void ceaseFire () { state = FiringState.Stopped; }

	public virtual void processFire() {}

	protected void setFiringArc(string arc, bool enabled) {
		transform.FindChild(arc).gameObject.SetActive (enabled);
	}

	protected void setFiringArcVisibility(string arc, bool visible) {
		transform.FindChild(arc).gameObject.GetComponent<SpriteRenderer> ().enabled = visible;
	}

	protected virtual List<string> getFiringArcs() { return null; }

	public void displayFiringArcs() {
		foreach (string token in getFiringArcs()) {
			transform.FindChild(token).GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	public void hideFiringArcs() {
		foreach (string token in getFiringArcs()) {
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
