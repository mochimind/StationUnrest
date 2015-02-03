using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShipMgr : MonoBehaviour {

	public GameObject _playerShip;

	private static PlayerShipMgr instance;
	private List<GameObject> ships;

	// Use this for initialization
	void Start () {
		ships = new List<GameObject> ();

		// instantiate a player ship at 0,0
		//GameObject addObj = (GameObject)Instantiate (_playerShip); 
		GameObject addObj = (GameObject)Instantiate(Resources.Load ("PlayerShip"));
		ships.Add(addObj);


		// lock in this instance
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static GameObject GetNearestShip(float x, float y) {
		return instance.ships[0];
	}
}
