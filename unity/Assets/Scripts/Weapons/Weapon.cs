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

	
	protected enum FiringState {
		GunCooldown, PulseCooldown, Pulsing, Ready, Stopped
	}

	// Use this for initialization
	void Start () {
	
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

	public bool inFiringSolution(Vector3 _targetPos) {
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
		Debug.Log ("this is inefficient, looks at all game objects a lot");
		foreach (GameObject targetable in Targeting.GetTargetableEnemies (resolution, transform.parent.tag)) {
			if (inFiringSolution(targetable.transform.position)) {
				target = targetable;
				return;
			}
		}
	}
}
