using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : Targetable {
	public int size;

	public List<GameObject> weapons = new List<GameObject> ();
	private List<GameObject>[] weaponResolutions = new List<GameObject>[4];

	public void handleClick() {
		foreach (GameObject token in weapons) {
			token.GetComponent<Weapon>().displayFiringArcs();
		}
	}

	public void handleUnclick() {
		foreach (GameObject token in weapons) {
			token.GetComponent<Weapon>().hideFiringArcs();
		}
	}
}
