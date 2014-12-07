using UnityEngine;
using System.Collections;

public class Processor : Building_UnitProduction {

	GameObject unit_1;

	// Use this for initialization
	void Start () {
		base.Start ();
	//	Debug.Log (isUnitBuilding+" "+isUpgradeBuilding);

	}
	
	// Update is called once per frame
	void Update () {
		if(isSelected) {
			if(Input.GetKeyDown(KeyCode.U)){
				bManScript.ExecuteOrder(buildTime,"unit_1");
			}
			if(Input.GetKeyDown(KeyCode.Y)){
				bManScript.ExecuteOrder(buildTime2,"unit_2");
			}
			if(Input.GetKeyDown(KeyCode.I)){
				bManScript.ExecuteOrder(buildTime2,"unit_3");
			}
		}

		base.Update ();
	}

	void OnGUI(){
		GUI.skin = guiScript.guiSkin;

		if (isSelected){

			float[] coord = guiScript.GetButtonCoordinates (0,0);

			//Debug.Log("FROM BUILDING! "+guiScript.GetButtonCoordinates ()[0]+ " "+coord[0]);
			if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "unit 1")){
				//Debug.Log("REAL BUTTON CLICKED");
				bManScript.ExecuteOrder(buildTime,"unit_1");
			}

			coord = guiScript.GetButtonCoordinates (1,0);

			if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "unit 2")){
			//	Debug.Log("REAL BUTTON CLICKED");
				bManScript.ExecuteOrder(buildTime2,"unit_2");
			}

			coord = guiScript.GetButtonCoordinates (2,0);
			
			if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "unit 3")){
				//	Debug.Log("REAL BUTTON CLICKED");
				bManScript.ExecuteOrder(buildTime2,"unit_3");
			}
		}
	}

}
