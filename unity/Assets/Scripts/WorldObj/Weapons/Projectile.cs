using UnityEngine;
using System.Collections;

public class Projectile : Destructable {
	public float speed;
	public int damage;
	private GameObject parent;

	void OnTriggerEnter2D(Collider2D collision) {
		Destructable d = collision.gameObject.GetComponent<Destructable> ();
		if (d!= null && collision.gameObject != parent) {
			d.handleDamage(damage);
			onDeath();
		}
	}

	public void setTarget(GameObject target, GameObject _parent) {
		Debug.Log ("we need to fix the this angle calculation, also add border to the stage");
		parent = _parent;
		float targetRotation = Angle.RotationAngle (transform, target.transform.position,-90f); 
		float startingRotation = transform.rotation.eulerAngles.z;
		if (startingRotation > 180) {
			startingRotation -= 360;
		}

		transform.position = _parent.transform.position;
		transform.rotation = Quaternion.AngleAxis (targetRotation + startingRotation, Vector3.forward);
		transform.rigidbody2D.velocity = (target.transform.position - transform.position).normalized * speed;
	}

	public override void onDeath () {
		// play animation

		base.onDeath ();
	}
}
