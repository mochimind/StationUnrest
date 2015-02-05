using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickHandler : MonoBehaviour {

	private GameObject target = null;
	private bool hasMoved;
	private bool isMoving;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			hasMoved = false;
			isMoving = true;
		} else if (Input.GetMouseButtonUp (0)) {
			isMoving = false;
			if (!hasMoved) {
				GUILayer l = (GUILayer)Camera.main.GetComponent<GUILayer> ();
				GUIElement gl = l.HitTest (Input.mousePosition);
				if (gl != null) {
					return;
				}
				
				// check if there's anything under the mouse button
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero); 
				if (hit.collider != null) {
					// check if it's a ship
					hit.collider.gameObject.BroadcastMessage ("clicked", SendMessageOptions.DontRequireReceiver);
					target = hit.collider.gameObject;
				} else if (target != null) {
					target.BroadcastMessage ("offTargetClick", Camera.main.ScreenToWorldPoint (Input.mousePosition), SendMessageOptions.DontRequireReceiver);
				}
			}
		} else if (isMoving) {
			float xMove = Input.GetAxis ("Mouse X");
			float yMove = Input.GetAxis ("Mouse Y");

			if (xMove != 0 || yMove != 0) {
				hasMoved = true;
				CameraController.AdjustCameraPosition(xMove,yMove);
			}
		}
	}


}
