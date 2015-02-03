﻿using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {

	private Vector3 destination;

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
		if (state == ThrusterState.Rotating) {
			if (targetRotation < 0) {
				curRotation -= getRotationSpeed();
				if (curRotation < targetRotation) {
					curRotation = targetRotation;
					if (faceTargetOnly) {
						state = ThrusterState.Stopped;
					} else {
						state = ThrusterState.Thrusting;
					}
				}
			} else {
				curRotation += getRotationSpeed();
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
			transform.parent.position = Vector2.MoveTowards(transform.parent.position, destination, getMovementSpeed());
			if (transform.parent.position == destination) {
				Debug.Log ("stopped moving");
				state = ThrusterState.Stopped;
			}

		}
	}

	public void startMove(Vector3 _destination, float rotationOffset) {
		destination = _destination;
		faceTargetOnly = false;
		initalizeMove (rotationOffset);
	}

	private void initalizeMove(float rotationalOffset) {
		state = ThrusterState.Rotating;
		Vector3 dir = destination - transform.position;
		targetRotation = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + rotationalOffset;
		Debug.Log ("dir: " + dir + "||" + targetRotation);
		startingRotation = transform.parent.rotation.eulerAngles.z;
		if (startingRotation > 180) {
			startingRotation -= 360;
		}
		curRotation = 0f;
		
		float angle = targetRotation - startingRotation;
		if (angle > 180) {
			angle -= 360;
		} else if (angle < -180) {
			angle += 360;
		}
		
		Debug.Log ("target " + targetRotation + ", start " + startingRotation + ", angle " + angle);
		if (Mathf.Abs (angle) < 3) {
			// know when to stop
			state = ThrusterState.Stopped;
		}
		targetRotation = angle;
	}

	public void startLook(Vector3 _destination, float rotationOffset) {
		destination = _destination;
		faceTargetOnly = true;
		initalizeMove (rotationOffset);
	}

	public void stopMove() {
		state = ThrusterState.Stopped;
	}

	public virtual float getRotationSpeed() {
		return 0f;
	}

	public virtual float getMovementSpeed() {
		return 0f;
	}
}