using UnityEngine;
using System.Collections;

public class FiringArcHandler : MonoBehaviour {
	private GameObject target;
	private TargetingArcListener listener;


	public interface TargetingArcListener {
		void targetEntered();
		void targetExited();
	}

	void Start() {
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}

	public void setTarget(GameObject _target, TargetingArcListener _listener) {
		listener = _listener;
		target = _target;

		// flash teh collider to get the collision triggers
		gameObject.GetComponent<Collider2D> ().enabled = false;
		gameObject.GetComponent<Collider2D> ().enabled = true;
	}

	public void unsetTarget() {
		listener = null;
		target = null;
		collider.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (target != null && collision.gameObject == target && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Alien")) {
			Debug.Log ("collision entered");
			listener.targetEntered();
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (target != null && collision.gameObject == target && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Alien")) {
			Debug.Log ("collision exited");
			listener.targetExited();
		}
	}
}
