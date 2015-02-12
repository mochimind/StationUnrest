using UnityEngine;
using System.Collections;

public class WeaponGroupButton : MonoBehaviour {

	public int slotNumber;
	private bool isActive;

	private WeaponGroupListener handler;

	void Start () {
		disable();
	}

	public void enable(WeaponGroupListener _handler) { 
		gameObject.SetActive(true);
		handler = _handler;
		activate();
	}

	public void disable() { 
		gameObject.SetActive(false);
		isActive = false;
	}

	public void activate() {
		isActive = true;
	}

	public void deactivate() {
		isActive = false;
	}
	
	public interface WeaponGroupListener {
		void WGActivated(int id);
		void WGDeactivated(int id);
	}

	void OnMouseUp() {
		if (isActive) {
			deactivate();
			handler.WGDeactivated(slotNumber);
		} else {
			activate();
			handler.WGActivated(slotNumber);
		}
	}
}
