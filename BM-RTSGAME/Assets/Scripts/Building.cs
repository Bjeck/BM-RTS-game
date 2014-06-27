using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

	GameObject buildMan;
	BuildingManager bManScript;

	// Use this for initialization
	void Start () {
		buildMan = GameObject.Find ("BuildingManager");
		bManScript = buildMan.GetComponent<BuildingManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//FOR PLACEMENT
	void OnTriggerEnter(Collider c){
		//Debug.Log (c.tag);
		if (c.tag == "Building" || c.tag == "Obstacle") {
			bManScript.colliders.Add(c);		
		}
	}

	void OnTriggerExit(Collider c){
		if (c.tag == "Building" || c.tag == "Obstacle") {
			bManScript.colliders.Remove(c);		
		}
	}
}
