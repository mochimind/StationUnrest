using UnityEngine;
using System.Collections.Generic;

public class Targeting {

	public enum TargetResolution {
		Small, 		// can target all ships
		Medium,		// can target corvettes and above
		Large,		// can target cruiser and above
		Capital		// can target battleship and above
	}

	public static List<GameObject> GetTargetableEnemies (int resolution, string _myFac) {
		List<GameObject> outList = new List<GameObject>();
		string faction;
		if (_myFac == Faction.PLAYER) {
			faction = Faction.ALIEN;
		} else {
			faction = Faction.PLAYER;
		}
		foreach (GameObject target in GameObject.FindGameObjectsWithTag(faction)) {
			if (target.GetComponent<Ship>().size >= resolution) {
				outList.Add(target);
			}
		}
		return outList;
	}
}

public class Faction{
	public static readonly string PLAYER = "Player";
	public static readonly string ALIEN = "Alien";
}

public class TargetResolution {
	public static readonly int SMALL = 1;
	public static readonly int MEDIUM = 2;
	public static readonly int LARGE = 3;
	public static readonly int CAPITAL = 4;
}
