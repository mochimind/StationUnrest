using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {
	public static float THRESHOLD = 0.05f;

	public float rotationSpeed;
	public float movementSpeed;

	private Vector2 destination;

	private bool faceTargetOnly = false;
	private bool rotateClockwise;
	private float startingRotation;
	private float curRotation;
	private float targetRotation;
	private ThrusterState state = ThrusterState.Stopped;

	private enum ThrusterState {
		Rotating, Stopped, Thrusting
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/* better approach, will implement later. Also, needed for force based movement
		// calculate the current angle adjustment required and distance to target
		Quaternion rotation = Quaternion.LookRotation(destination - transform.position);
		if (rotation.eulerAngles.z < THRESHOLD) {
			
		}
		
		// if within thruster angle, apply force
		
		// rotate to face the location
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

		*/

		if (state == ThrusterState.Rotating) {
			if (targetRotation < 0) {
				curRotation -= rotationSpeed;
				if (curRotation < targetRotation) {
					curRotation = targetRotation;
					if (faceTargetOnly) {
						state = ThrusterState.Stopped;
					} else {
						state = ThrusterState.Thrusting;
					}
				}
			} else {
				curRotation += rotationSpeed;
				if (curRotation > targetRotation) {
					curRotation = targetRotation;
					if (faceTargetOnly) {
						state = ThrusterState.Stopped;
					} else {
						state = ThrusterState.Thrusting;
					}
				}
			}

			transform.parent.rotation = Quaternion.AngleAxis (curRotation + startingRotation, Vector3.forward);
		} else if (state == ThrusterState.Thrusting) {
			transform.parent.position = Vector2.MoveTowards(transform.parent.position, destination, movementSpeed);
			if (Vector2.Distance(new Vector2(transform.parent.position.x, transform.parent.position.y), new Vector2(destination.x, destination.y)) <= THRESHOLD) {
				state = ThrusterState.Stopped;
			}

		}
	}

	public void startMove(Vector3 _destination, float rotationOffset) {
		destination = _destination;
		faceTargetOnly = false;
		initalizeMove (rotationOffset);
	}

	public void keepDistance (Vector3 _destination, float rotationOffset, float _distance) {
		destination = Targeting.LerpByDistance(_destination, transform.position, _distance);
		faceTargetOnly = false;
		initalizeMove (rotationOffset);
	}

	private void initalizeMove(float rotationalOffset) {
		state = ThrusterState.Rotating;
		targetRotation = Angle.RotationAngle (transform.parent, destination, rotationalOffset);
		startingRotation = transform.parent.rotation.eulerAngles.z;
		if (startingRotation > 180) {
			startingRotation -= 360;
		}
		curRotation = 0f;
		if (Mathf.Abs (targetRotation) < 3) {
			// know when to stop
			state = ThrusterState.Stopped;
		}
	}

	public void startLook(Vector3 _destination, float rotationOffset) {
		destination = _destination;
		faceTargetOnly = true;
		initalizeMove (rotationOffset);
	}

	public void stopMove() {
		state = ThrusterState.Stopped;
	}
}
