using UnityEngine;
using System.Collections;

public class WeaponGroup : MonoBehaviour, WeaponGroupButton.WeaponGroupListener {

	private static WeaponGroup instance;


	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void ShowWeaponGroups(bool slot1, bool slot2, bool slot3, bool slot4) {
		if (slot1) { instance.transform.FindChild("WeaponGroup1").GetComponent<WeaponGroupButton>().activate(); }
		if (slot2) { instance.transform.FindChild("WeaponGroup2").GetComponent<WeaponGroupButton>().activate(); }
		if (slot3) { instance.transform.FindChild("WeaponGroup3").GetComponent<WeaponGroupButton>().activate(); }
		if (slot4) { instance.transform.FindChild("WeaponGroup4").GetComponent<WeaponGroupButton>().activate(); }
	}

	public static void HideWeaponGroups() {
		instance.transform.FindChild("WeaponGroup1").GetComponent<WeaponGroupButton>().deactivate();
		instance.transform.FindChild("WeaponGroup2").GetComponent<WeaponGroupButton>().deactivate();
		instance.transform.FindChild("WeaponGroup3").GetComponent<WeaponGroupButton>().deactivate();
		instance.transform.FindChild("WeaponGroup4").GetComponent<WeaponGroupButton>().deactivate();
	}

	void WeaponGroupButton.WeaponGroupListener.WGClicked (int id) {

	}
}
