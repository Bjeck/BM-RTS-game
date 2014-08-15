using UnityEngine;
using System.Collections;

public class Building_Upgrade : Building {

	public float upgradeTime = 10f;


	// Use this for initialization
	public void Start () {
		base.Start ();
		isUpgradeBuilding = true;
		Debug.Log("from upgrade: "+isUpgradeBuilding);
	}
	
	// Update is called once per frame
	public void Update () {
		base.Update ();
	
	}

	public IEnumerator ConstructUpgrade(float time, string uName){
		isConstructing = true;
		buildingLight = GetComponent<Light> ();
		buildingLight.intensity = 0;
		float t = 0;
		
		while(t<time){
			//Debug.Log(t);
			buildingLight.intensity += t;
			buildingLight.intensity /= 9;
			t+=Time.deltaTime;
			yield return 0;
		}
		//SpawnUnit (uName);
		Debug.Log ("UPGRADE ACTIVE!");
		isConstructing = false;
		buildingLight.intensity = 0;
		yield return 0;
	}


}
