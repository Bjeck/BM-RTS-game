using UnityEngine;
using System.Collections;

public class Building_Upgrade : Building {

	float upgradeTime = 2f;
	bool isSelectingUnitForUpgrade = false;
	string currentNameOfUpgrade;


	// Use this for initialization
	public void Start () {
		base.Start ();
		isUpgradeBuilding = true;
	}
	
	// Update is called once per frame
	public void Update () {
		base.Update ();
	
	}

	public IEnumerator ConstructUpgrade(float time, string name){ //Is called by buildingmanager when an upgrade is pressed. This allows the unit selection GUI to appear.
		isSelectingUnitForUpgrade = true;
		upgradeTime = time;
		currentNameOfUpgrade = name;
		yield return 0;

	}



	public IEnumerator LoadUpgradeOntoUnit(string uName){ //When a unit has been selected as well, this Coroutine starts, goes until time is done, and then upgrades the ability to all units of the set type.

		isConstructing = true;
		buildingLight = GetComponent<Light> ();
		buildingLight.intensity = 0;
		float t = 0;

		while(t<upgradeTime){
			//Debug.Log(t);
			buildingLight.intensity += t;
			buildingLight.intensity /= 9;
			t+=Time.deltaTime;
			yield return 0;
		}
		//SpawnUnit (uName);
		string fullStringForName = "Abilities/"+currentNameOfUpgrade;

		foreach (GameObject g in UnitManager.instance.unitsInGame) { //Give all active units the ability.
			if(g.GetComponent<Unit>().identifier == uName){
				//Debug.Log(fullStringForName);
				GameObject upgradeAbility = (GameObject)Network.Instantiate(Resources.Load(fullStringForName,typeof(GameObject)), transform.position, Quaternion.identity, 0);
				upgradeAbility.transform.parent = g.transform;
			}		
		}
		UnitManager.instance.SetAbilityForUnit (fullStringForName, uName); //Send ability info to unit manager, so next units get ability too.

		isConstructing = false;
		buildingLight.intensity = 0;
		isSelectingUnitForUpgrade = false;
		currentNameOfUpgrade = null;
		yield return 0;
	}




	void OnGUI(){ 
		GUI.skin = guiScript.guiSkin;
		if (isSelected){
			float[] coord = guiScript.GetButtonCoordinates (0,0);

			if(isSelectingUnitForUpgrade){ //If you've already selected an upgrade, and wants to select a unit
				coord = guiScript.GetButtonCoordinates (0,0);
				
				//Debug.Log("FROM BUILDING! "+guiScript.GetButtonCoordinates ()[0]+ " "+coord[0]);
				if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "UPG: unit 1")){
					//Debug.Log("REAL BUTTON CLICKED");
					//bManScript.ExecuteOrder(buildTime,"unit_1");
					StartCoroutine(LoadUpgradeOntoUnit("unit_1"));
				}
				
				coord = guiScript.GetButtonCoordinates (1,0);
				
				if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "UPG: unit 2")){
					//	Debug.Log("REAL BUTTON CLICKED");
					StartCoroutine(LoadUpgradeOntoUnit("unit_2"));
				}
				
				coord = guiScript.GetButtonCoordinates (2,0);
				
				if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "UPG: unit 3")){
					//	Debug.Log("REAL BUTTON CLICKED");
					StartCoroutine(LoadUpgradeOntoUnit("unit_3"));
				}
			}


			else{ //If you haven't selected an upgrade yet, run this (selecting the abilities)
				//Debug.Log("FROM BUILDING! "+guiScript.GetButtonCoordinates ()[0]+ " "+coord[0]);
				if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "zap!")){
					bManScript.UpgradeSomething(upgradeTime,"Zap");
				}

				coord = guiScript.GetButtonCoordinates (1,0);

				if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "Mine")){
					bManScript.UpgradeSomething(upgradeTime,"LayMine");
				}
			}
		}
	}


}
