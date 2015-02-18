using UnityEngine;
using System.Collections;

public class CruiserDefaultThruster : Thruster {
	public float rotateSpeed;
	public float thrustSpeed;
	
	public override float getMovementSpeed () {
		return thrustSpeed;
	}
	
	public override float getRotationSpeed () {
		return rotateSpeed;
	}
}
