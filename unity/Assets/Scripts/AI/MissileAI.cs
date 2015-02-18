using UnityEngine;
using System.Collections;

public class MissileAI : AI {
	protected override void onTargetDeath () {
		Targetable t = gameObject.GetComponent<Targetable>();
		if (t == null) {
			Debug.Log ("Error: Missiles need to have targetable script");
			Destroy (gameObject);
		}
		t.onDeath();
	}
}
