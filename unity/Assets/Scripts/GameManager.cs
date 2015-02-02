using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private bool init = false;
	public GameObject _alienScout;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!init) {
			init = true;
			GameObject alienScout = (GameObject) Instantiate (_alienScout);
			alienScout.transform.position = new Vector2(-3f, -3f);
		}
	}
}
