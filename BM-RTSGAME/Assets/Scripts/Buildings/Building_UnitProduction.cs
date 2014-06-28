using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building_UnitProduction : Building {

	public List<Unit> units = new List<Unit>();
	public Vector3 Waypoint;

	// Use this for initialization
	void Start () {
		Vector3 temp = transform.position;
		temp.y += 5;
		Waypoint = temp;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
