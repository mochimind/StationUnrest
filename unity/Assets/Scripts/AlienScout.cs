using UnityEngine;
using System.Collections;

public class AlienScout : MonoBehaviour, Ship.Targeter {

	private GameObject target = null;
	public float cooldownTime = 1f;
	public float fireTime = 0.8f;
	public int damage = 5;

	private float timeTillFire = -1;
	private float timeTillStopFire = 10000000;
	private LineRenderer laser;

	// Use this for initialization
	void Start () {
		// determine the nearest player ship
		target = PlayerShipMgr.GetNearestShip (transform.position.x, transform.position.y);
		target.GetComponent<Ship> ().handleTarget (this);

		// rotate to face target
		faceTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
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

				// deal damage to the target
			} else if (timeTillStopFire <= fireTime) {
				updateFireLine ();
			}
		}
	}

	private void faceTarget() {
		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	private void fireAtTarget() {
		laser = gameObject.AddComponent<LineRenderer> ();
		laser.material = new Material (Shader.Find ("Particles/Additive"));
		laser.SetColors (Color.red, Color.red);
		laser.SetWidth (0.1f, 0.1f);
		laser.SetVertexCount (2);
		target.GetComponent<Ship> ().handleDamage (damage);
	}

	private void updateFireLine() {
		laser.SetPosition(0, transform.position);
		laser.SetPosition (1, target.transform.position);
	}

	private void stopFiring() {
		Destroy (laser);
	}

	void Ship.Targeter.handleDeath() {
		target = null;
		Debug.Log ("todo: implement switching targets");
	}
}
