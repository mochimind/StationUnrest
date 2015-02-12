using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickHandler : MonoBehaviour {

	public float threshold = 0.1f;


	private Clickable target = null;
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
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				RaycastHit2D[] hits = Physics2D.RaycastAll (mousePos, Vector2.zero); 
				foreach (RaycastHit2D hit in hits) {
					if (hit.collider != null) {
						Clickable c = hit.collider.gameObject.GetComponent<Clickable>();
						if (c == null) { continue; }
						if (target != null) {
							if (target.handleClick(hit.collider.gameObject, mousePos)) {
								return;
							} else {
								target.loseClickFocus();
							}
						}
						target = c;
						target.clicked ();
						return;
					}
				}
				// three were no clickable items clicked on
				if (target != null) {
					target.handleClick(null, mousePos);
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
