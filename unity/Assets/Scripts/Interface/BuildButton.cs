using UnityEngine;
using System.Collections;

public class BuildButton : MonoBehaviour {
	void OnMouseDown() {
		GUIMgr.ToggleBuildMenu ();
	}
}
