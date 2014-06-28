using UnityEngine;
using System.Collections;

public class Processor : Building_UnitProduction {

	GameObject unit_1;


	// Use this for initialization
	void Start () {
		Vector3 temp = transform.position;
		temp.y += 5;
		Waypoint = temp;

	}
	
	// Update is called once per frame
	void Update () {
		//if (isSelected) {
				//}
			//Debug.Log ("I AM SELECTED PROCESSOR!");
			//OnGUI ();
	}

	void OnGUI(){
		if(isSelected)
		if(GUI.Button (new Rect (0, Screen.height - 50, 100, 50), "Build Unit 1")){
			unit_1 = (GameObject)Instantiate(Resources.Load("unit_1",typeof(GameObject)));
			unit_1.transform.position = transform.position;
			Pathfindinger path = unit_1.GetComponent<Pathfindinger>();
			path.targetPosition = Waypoint;
		}
	}
}
