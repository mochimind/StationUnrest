using UnityEngine;
using System.Collections;

public class WeaponGroupButton : MonoBehaviour {

	public int slotNumber;

	private WeaponGroupListener handler;

	void Start () {
		gameObject.SetActive(false);
	}

	public void activate() { gameObject.SetActive(true); }

	public void deactivate() { gameObject.SetActive(false); }
	
	public interface WeaponGroupListener {
		void WGClicked(int id);
	}

	void OnMouseUp() {
		handler.WGClicked(slotNumber);
	}
}
