using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickHandler : MonoBehaviour {

	public float threshold = 0.1f;


	private GameObject target = null;
	private Vector2 moveDelta;
	private bool isMoving;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			moveDelta = new Vector2(0f, 0f);
			isMoving = true;
		} else if (Input.GetMouseButtonUp (0)) {
			isMoving = false;
			if (Mathf.Abs (moveDelta.x) < threshold && Mathf.Abs(moveDelta.y) < threshold) {
				GUILayer l = (GUILayer)Camera.main.GetComponent<GUILayer> ();
				GUIElement gl = l.HitTest (Input.mousePosition);
				if (gl != null) {
					return;
				}
				
				// check if there's anything under the mouse button
				RaycastHit2D[] hits = Physics2D.RaycastAll (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero); 
				foreach (RaycastHit2D hit in hits) {
					if (hit.collider != null) {
						// check if it's a ship
						if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Alien") {
							hit.collider.gameObject.BroadcastMessage ("clicked", SendMessageOptions.DontRequireReceiver);
							target = hit.collider.gameObject;
							return;
						}
					}
				}

				// at this point, we didn't find a ship under the button click
				if (target != null) {
					target.BroadcastMessage ("offTargetClick", Camera.main.ScreenToWorldPoint (Input.mousePosition), SendMessageOptions.DontRequireReceiver);
				}
			}
		} else if (isMoving) {
			float xMove = Input.GetAxis ("Mouse X");
			float yMove = Input.GetAxis ("Mouse Y");
			moveDelta = new Vector2(moveDelta.x + xMove, moveDelta.y + yMove);

			if (xMove != 0 || yMove != 0) {
				CameraController.AdjustCameraPosition(xMove,yMove);
			}
		}
	}


}
