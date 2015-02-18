using UnityEngine;
using System.Collections;

public class Propelled : MonoBehaviour {
	public GameObject equipment;
	private GameObject engine;

	public float rotationOffset;

	void Start() {
		if (equipment != null) {
			equip(equipment);
		}
	}

	public void equip(GameObject _engine) {
		engine = (GameObject) Instantiate(equipment);
		engine.transform.position = transform.position;
		engine.transform.parent = transform;
	}

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
