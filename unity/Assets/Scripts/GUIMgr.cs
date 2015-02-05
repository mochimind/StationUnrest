using UnityEngine;
using System.Collections;

public class GUIMgr : MonoBehaviour {

	private static GUIMgr instance;
	private GameObject selectedShip = null;
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
		GameObject shipDisplay = transform.FindChild ("Ship").gameObject;
		if (selectedShip == null) {
			shipDisplay.SetActive(false);
			transform.FindChild("Interface").gameObject.SetActive(false);
		} else {
			shipDisplay.SetActive(true);
			transform.FindChild("Interface").gameObject.SetActive(true);
			GameObject healthBack = shipDisplay.transform.FindChild("HealthBack").gameObject;
			float percentHealth = selectedShip.GetComponent<Ship>().getPercentHealth();
			if (percentHealth >= 0f) {
				healthBack.transform.position = new Vector3(0.25f - (1f - percentHealth) * 0.1f, healthBack.transform.position.y, healthBack.transform.position.z);
			}
		}
	}

	public static void SelectShip(GameObject ship) {
		instance.selectedShip = ship;
	}

	public static void SelectNothing() {
		instance.selectedShip = null;
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
			GUIMgr.OpenBuildMenu ();
		} else {
			GUIMgr.CloseBuildMenu ();
		}
	}
}
