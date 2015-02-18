using UnityEngine;
using System.Collections;

public class Propelled : MonoBehaviour {
	public GameObject engine;

	public float rotationOffset;

	public void moveToCoords(Vector3 mousePos) {
		engine.GetComponent<Thruster> ().startMove (mousePos, rotationOffset);
		/*
		foreach (Targeter t in watchers) {
			t.handleTargetMove(mousePos);
		}
		*/
	}
	
	public void maintainDistance(Vector3 _location, float _dist) {
		engine.GetComponent<Thruster> ().keepDistance (_location, rotationOffset, _dist);
		/*
		foreach (Targeter t in watchers) {
			t.handleTargetMove(_location);
		}
		*/
	}
	
	public void looktAt(Vector3 location) {
		engine.GetComponent<Thruster> ().startLook (location, rotationOffset);
	}
}
