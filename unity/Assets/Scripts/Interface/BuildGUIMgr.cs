using UnityEngine;
using System.Collections;

public class BuildGUIMgr : MonoBehaviour {

	private static BuildGUIMgr instance;
	private BuildScreen curScreen = BuildScreen.Housing;
	private bool buildScreenVisible = false;
	
	private enum BuildScreen {
		Housing, Food, Manufacturing, Weapons, Production
	}
	

	// Use this for initialization
	void Start () {
		instance = this;
		CloseBuildMenu ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public static void OpenBuildMenu() {
		instance.transform.FindChild ("BuildScreen").gameObject.SetActive(true);
		instance.buildScreenVisible = true;
	}
	
	public static void CloseBuildMenu() {
		instance.transform.FindChild ("BuildScreen").gameObject.SetActive(false);
		instance.buildScreenVisible = false;
	}
	
	public static void ToggleBuildMenu() {
		if (!instance.buildScreenVisible) {
			BuildGUIMgr.OpenBuildMenu ();
		} else {
			BuildGUIMgr.CloseBuildMenu ();
		}
	}
}
