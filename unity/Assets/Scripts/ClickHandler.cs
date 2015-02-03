﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			// check if there's anything under the mouse button
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 
			if (hit.collider != null) {
				// check if it's a ship
				hit.collider.gameObject.BroadcastMessage("clicked", SendMessageOptions.DontRequireReceiver);
			} else {
				GUIMgr.SelectNothing();
			}
		}
	}

}