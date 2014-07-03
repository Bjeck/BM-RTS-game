using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building_UnitProduction : Building {

	public List<Unit> units = new List<Unit>(); //not used
	public Vector3 Waypoint;
	public float buildTime = 1;
	public string unitName;

	// Use this for initialization
	public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	public void Update () {
		base.Update ();
	}

	public IEnumerator ConstructUnit(float time, string uName){
		isConstructing = true;
		buildingLight = GetComponent<Light> ();
		buildingLight.intensity = 0;
		float t = 0;
		while(t<buildTime){
			Debug.Log(t);
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
		GameObject unit = (GameObject)Instantiate(Resources.Load(name,typeof(GameObject)));
		unit.transform.position = transform.position;
		Pathfindinger path = unit.GetComponent<Pathfindinger>();
		path.targetPosition = Waypoint;
	}
}
