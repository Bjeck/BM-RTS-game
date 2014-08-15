using UnityEngine;
using System.Collections;

public class upgrade_1 : Building_Upgrade {

	// Use this for initialization
	void Start () {
		base.Start ();
		Debug.Log (isUpgradeBuilding);
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}


	void OnGUI(){
		GUI.skin = guiScript.guiSkin;
		
		if (isSelected){
			float[] coord = guiScript.GetButtonCoordinates (0,0);
			
			//Debug.Log("FROM BUILDING! "+guiScript.GetButtonCoordinates ()[0]+ " "+coord[0]);
			if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "upgrade!")){
				Debug.Log("REAL BUTTON CLICKED");
				bManScript.UpgradeSomething(upgradeTime,"upgrade_1");
			}
		}
	}

}
