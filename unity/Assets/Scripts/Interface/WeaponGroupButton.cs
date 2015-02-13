using UnityEngine;
using System.Collections;

public class WeaponGroupButton : MonoBehaviour {

	public int slotNumber;
	private bool isActive;

	public Texture enableTexture;
	public Texture disableTexture;

	private WeaponGroupListener handler;

	void Start () {
		disable();
	}

	public void enable(WeaponGroupListener _handler) { 
		gameObject.SetActive(true);
		handler = _handler;
		deactivate();
	}

	public void disable() { 
		gameObject.SetActive(false);
		isActive = false;
	}

	public void activate() {
		isActive = true;
		gameObject.GetComponent<GUITexture> ().texture = enableTexture;
	}

	public void deactivate() {
		isActive = false;
		gameObject.GetComponent<GUITexture> ().texture = disableTexture;
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
