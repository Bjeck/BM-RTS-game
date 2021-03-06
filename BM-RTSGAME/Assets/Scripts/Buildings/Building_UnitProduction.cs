﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building_UnitProduction : Building {

	public List<Unit> units = new List<Unit>(); //not used
	public float buildTime = 1;
	public float buildTime2 = 1.5f;
	public string unitName;


	// Use this for initialization
	public void Start () {
		base.Start ();
		isUnitBuilding = true;
		SetWaypoint (transform.position);
	}
	
	// Update is called once per frame
	public void Update () {
	//	Debug.Log ("transform: " + transform.position + " waypoint: " + Waypoint);
		base.Update ();

	

	}

	public IEnumerator ConstructUnit(float time, string uName){
		isConstructing = true;
		buildingLight = GetComponent<Light> ();
		buildingLight.intensity = 0;
		float t = 0;

		while(t<time){
			//Debug.Log(t);
			buildingLight.intensity += t;
			buildingLight.intensity /= 3;
			t+=Time.deltaTime;
			yield return 0;
		}
		SpawnUnit (uName);
		isConstructing = false;
		buildingLight.intensity = 0;
		yield return 0;
	}

	public void SpawnUnit(string name){
		GameObject unit = (GameObject)Network.Instantiate(Resources.Load(name,typeof(GameObject)), transform.position, Quaternion.identity, 0);
		unit.GetComponent<Unit>().player1 = player1;
		unit.transform.position = transform.position;



		StartCoroutine (waitAndMoveToWayPoint (unit));
	}

	IEnumerator waitAndMoveToWayPoint(GameObject unit){ //Had to make a slight delay on the waypoint thing because the pathfinder script did apparently not exist at the exact moment of creation of the unit.
		float t = 0;
		while(t<0.01f){
			t+=Time.deltaTime;
			yield return 0;
		}
		Pathfindinger path = unit.GetComponent<Pathfindinger>();
		path.SetPath (Waypoint);
		yield return 0;
	}


	public override void SetWaypoint(Vector3 point){
	//	Debug.Log ("POINT: "+point);
		Waypoint = point;
		if (wayPointMarker != null) {
			wayPointMarker.transform.position = Waypoint;		
		}
	}


}
