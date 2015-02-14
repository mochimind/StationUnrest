using UnityEngine;
using System.Collections;

public class FrontFireAttack : AI {
	protected override bool acquireTarget () {
		target = PlayerShipMgr.GetNearestShip (transform.position.x, transform.position.y);
		target.GetComponent<Targetable> ().handleTarget (this);
		return base.acquireTarget ();
	}
}
