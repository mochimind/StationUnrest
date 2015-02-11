using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour, Targeter {

	protected GameObject target = null;
	protected AIState state = AIState.acquiring;
	public float targetDistanceThreshold;
	public float moveCalcDelay = 0.5f;

	protected Vector2 targetPos;
	protected float moveTimer = -1f;

	protected enum AIState {
		acquiring, pursuing, fleeing, stopped
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (state == AIState.acquiring) {
			if (!acquireTarget()) { 
				state = AIState.stopped;
			} else {
				state = AIState.pursuing;
				moveTimer = moveCalcDelay;
			}
		} else if (state == AIState.pursuing) {
			// if the target has moved a certain amount, then recalibrate movement
			if (moveTimer >= moveCalcDelay && Vector2.Distance(transform.position, target.transform.position) > targetDistanceThreshold) {
				moveTimer = 0f;
				gameObject.GetComponent<Ship>().maintainDistance(target.transform.position, targetDistanceThreshold);
			} else {
				moveTimer += Time.deltaTime;
			}
		}
	}

	void Targeter.handleTargetDeath() { state = AIState.acquiring; }
	void Targeter.handleTargetMove(Vector3 pos) { }

	protected virtual bool acquireTarget() {
		if (target == null) {
			return false;
		}

		foreach (GameObject weaponObj in gameObject.GetComponent<Ship>().weapons) {
			Weapon weapon = weaponObj.GetComponent<Weapon>();
			weapon.setTarget(target);
		}

		return true;
	}
}
