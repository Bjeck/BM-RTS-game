using UnityEngine;
using System.Collections;

public class Processor : Building_UnitProduction {

	GameObject unit_1;

	// Use this for initialization
	void Start () {
		base.Start ();
		Vector3 temp = transform.position;
		temp.y += 5;
		Waypoint = temp;

	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void OnGUI(){
		if(isSelected)
		if(GUI.Button (new Rect (0, Screen.height - 50, 100, 50), "Build Unit 1")){
			if(!isConstructing){
				unitName = "unit_1";
				StartCoroutine(ConstructUnit(buildTime, unitName));
			}
		}
	}


	
}
