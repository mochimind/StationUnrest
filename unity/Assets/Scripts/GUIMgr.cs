using UnityEngine;
using System.Collections;

public class GUIMgr : MonoBehaviour {

	private static GUIMgr instance;
	private GameObject selectedShip = null;


	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject shipDisplay = transform.FindChild ("Ship").gameObject;
		if (selectedShip == null) {
			shipDisplay.SetActive(false);
		} else {
			shipDisplay.SetActive(true);
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
}
