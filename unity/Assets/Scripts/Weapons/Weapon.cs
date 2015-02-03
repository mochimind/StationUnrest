using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour, Ship.Targeter {

	protected GameObject target; 

	protected bool canFire = false;

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

	void Ship.Targeter.handleDeath() {
		ceaseFire ();
		Debug.Log ("todo: implement switching targets");
	}

}
