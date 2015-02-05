using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private static CameraController instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void AdjustCameraPosition(float deltaX, float deltaY) {
		instance.transform.position = new Vector3 (instance.transform.position.x - deltaX,
		                                          instance.transform.position.y - deltaY, instance.transform.position.z);
	}
}
