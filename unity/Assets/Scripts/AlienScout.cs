using UnityEngine;
using System.Collections;

public class AlienScout : MonoBehaviour {

	private GameObject target;
	public float cooldownTime = 5f;
	public float fireTime = 4f;

	private float timeTillFire = -1;
	private float timeTillStopFire = 10000000;
	private LineRenderer laser;

	// Use this for initialization
	void Start () {
		// determine the nearest player ship
		target = PlayerShipMgr.GetNearestShip (transform.position.x, transform.position.y);

		// rotate to face target
		faceTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		// rotate towards target

		// move towards target

		// if in range fire gun

		// get the amount of time since last fire
		timeTillFire -= Time.deltaTime;
		timeTillStopFire -= Time.deltaTime;

		if (timeTillFire <= 0f) {
			fireAtTarget ();
			timeTillFire = cooldownTime;
			timeTillStopFire = fireTime;
		} else if (timeTillStopFire <= 0f) {
			stopFiring ();
			timeTillStopFire = 1000000;
		} else if (timeTillStopFire <= fireTime) {
			updateFireLine();
		}
	}

	private void faceTarget() {

	}

	private void fireAtTarget() {
		laser = gameObject.AddComponent<LineRenderer> ();
		laser.material = new Material(Shader.Find("Particles/Additive"));
		laser.SetColors (Color.red, Color.red);
		laser.SetWidth (0.1f, 0.1f);
		laser.SetVertexCount (2);
	}

	private void updateFireLine() {
		laser.SetPosition(0, transform.position);
		laser.SetPosition (1, target.transform.position);
	}

	private void stopFiring() {
		Destroy (laser);
	}
}
