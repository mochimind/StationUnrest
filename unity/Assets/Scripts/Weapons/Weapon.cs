using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour, Ship.Targeter {

	public GameObject target; 

	protected bool canFire = false;

	public int damage;
	public float firingDelay;
	public float firingTime;
	public int pulseCount;
	public float pulseDelay;
	public float firingArc;	// degrees
	public float maxRange;
	public float gunRotation;
	public int resolution;

	protected FiringState state = FiringState.Ready;
	protected int pulses;
	protected float nextStateCountdown = 0f;
	protected bool hasFiringSolution = false;
	private bool targetMovingOutOfRange = false;


	protected enum FiringArc {
		Top, Bottom, Left, Right
	}
	
	protected enum FiringState {
		GunCooldown, PulseCooldown, Pulsing, Ready, Stopped
	}

	// Use this for initialization
	void Start () {
		disableAllFiringArc ();
		enableFiringArcs ();
		//hideFiringArcs ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canFire) {
			processFire();
		}
	}
	
	public void setTarget(GameObject _target) {
		target = _target;
		target.GetComponent<Ship> ().handleTarget (this);
	}

	public void startFire () {
		canFire = true;
	}

	public virtual void ceaseFire () {
		target = null;
		canFire = false;
	}

	public virtual void processFire() {}

	void Ship.Targeter.handleTargetDeath() {
		ceaseFire ();
		Debug.Log ("todo: implement switching targets");
	}

	void Ship.Targeter.handleTargetMove(Vector3 _target) {
		if (!inFiringSolution (_target)) {
			targetMovingOutOfRange = true;
		} else {
			targetMovingOutOfRange = false;
		}
	}

	public bool inFiringSolution(Vector3 _targetPos) {
		Debug.Log ("firing arc really inefficient");
		// check if in firing arc
		float offsetAngle = gunRotation + transform.parent.gameObject.GetComponent<Ship> ().rotationOffset;
		if (Mathf.Abs(Angle.RotationAngle (transform.parent, _targetPos, offsetAngle)) > firingArc) {
			return false;
		}
		
		// check if in firing range
		float curDist = Vector2.Distance (new Vector2(transform.parent.position.x, transform.parent.position.y), 
		                                  new Vector2(_targetPos.x, _targetPos.y));
		if (curDist > maxRange) {
			return false;
		} else {
			return true;
		}
	}

	public void autoTarget() {
		// acquire a target
		Debug.Log ("this is inefficient, looks at all game objects a lot");
		foreach (GameObject targetable in Targeting.GetTargetableEnemies (resolution, transform.parent.tag)) {
			if (inFiringSolution(targetable.transform.position)) {
				target = targetable;
				return;
			}
		}
	}

	protected void disableAllFiringArc() {
		transform.FindChild ("FiringArcTop").gameObject.SetActive (false);
		transform.FindChild ("FiringArcBottom").gameObject.SetActive (false);
		transform.FindChild ("FiringArcLeft").gameObject.SetActive (false);
		transform.FindChild ("FiringArcRight").gameObject.SetActive (false);
	}

	protected void setFiringArc(FiringArc arc, bool enabled) {
		getArcGameObj(arc).SetActive (enabled);
	}

	protected GameObject getArcGameObj(FiringArc arc) {
		if (arc == FiringArc.Top) {
			return transform.FindChild("FiringArcTop").gameObject;
		} else if (arc == FiringArc.Bottom) {
			return transform.FindChild("FiringArcBottom").gameObject;
		} else if (arc == FiringArc.Left) {
			return transform.FindChild("FiringArcLeft").gameObject;
		} else {
			return transform.FindChild("FiringArcRight").gameObject;
		}
	}

	protected void setFiringArcVisibility(FiringArc arc, bool visible) {
		getArcGameObj (arc).GetComponent<SpriteRenderer> ().enabled = visible;
	}

	public virtual void showFiringArcs() { }

	public virtual void hideFiringArcs() { }

	protected virtual void enableFiringArcs() { }
}
