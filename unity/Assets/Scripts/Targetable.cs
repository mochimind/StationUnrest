using UnityEngine;
using System.Collections.Generic;

public class Targetable : Destructable {
	public List<Targeter> watchers  = new List<Targeter> ();
	public void handleTarget(Targeter t) {
		watchers.Add (t);
	}
	
	public void handleStopTarget(Targeter t) {
		watchers.Remove (t);
	}

	public override void onDeath () {
		foreach (Targeter t in watchers) {
			t.handleTargetDeath();
		}

		base.onDeath ();
	} 
}
