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
		if(isSelected)
		if(Input.GetKeyDown(KeyCode.U)){
			bManScript.ExecuteOrder(buildTime,"unit_1");
		}

		base.Update ();
	}

	void OnGUI(){
		if(isSelected)
		if(GUI.Button (new Rect (0, Screen.height - 300, 100, 50), "Build Unit 1")){
				unitName = "unit_1";
				bManScript.ExecuteOrder(buildTime,unitName);
		}
	}


	
}
