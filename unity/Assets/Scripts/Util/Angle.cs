using UnityEngine;

public class Angle {
	public static float RotationAngle(Transform baseObj, Vector3 targetCoords, float rotationalOffset) {
		Debug.Log ("we got: " + baseObj + targetCoords + rotationalOffset);
		// what's the rotational angle required to rotate base to face target
		Vector3 dir = targetCoords - baseObj.position;
		float targetRotation = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + rotationalOffset;
		float startingRotation = baseObj.rotation.eulerAngles.z;
		if (startingRotation > 180) {
			startingRotation -= 360;
		}

		float angle = targetRotation - startingRotation;
		if (angle > 180) {
			angle -= 360;
		} else if (angle < -180) {
			angle += 360;
		}

		//Debug.Log ("target " + targetRotation + ", start " + startingRotation + ", angle " + angle);
		return angle; 
	}

	public static float RotationalOffset(Transform baseObj, float offset) {
		return baseObj.rotation.eulerAngles.z + offset;
	}
}
