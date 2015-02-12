using UnityEngine;
using System.Collections;



public class Clickable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void clicked() {

	}

	public virtual bool handleClick(GameObject newObj, Vector3 mousePos) {
		return false;
	}

	public virtual void loseClickFocus() { }
}
