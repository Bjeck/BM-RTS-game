using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

	GameObject buildMan;
	BuildingManager bManScript;
	public bool isSelected = false;
	public bool isBuilding = false;
	public bool isConstructing = false;
	public SpriteRenderer sprtR;
	public Light buildingLight;
	Mouse mouseScript;

	// Use this for initialization
	public void Start () {
		Debug.Log ("BUILDING HERE!");
		buildMan = GameObject.Find ("BuildingManager");
		bManScript = buildMan.GetComponent<BuildingManager> ();
		mouseScript = GameObject.Find("Main Camera").GetComponent<Mouse> ();
	}
	
	// Update is called once per frame
	public void Update () {
	
		if (renderer.isVisible && Input.GetMouseButton (0) && mouseScript.isDrawingBox() && !bManScript.isDragging) { //for selection box. makes itself selection if it is inside the box.
			Vector3 camPos = Camera.main.WorldToScreenPoint (transform.position);
			camPos.y = Mouse.InvertMouseY (camPos.y);
			Debug.Log(mouseScript.selection.x+" "+mouseScript.selection.y);
			if(mouseScript.selection.Contains (camPos))
				mouseScript.AddBuildingSelection(this);
			if(!mouseScript.selection.Contains (camPos))
				mouseScript.RemoveBuildingSelection(this);
		}
		if (mouseScript.unitsSelected.Count > 0)
			mouseScript.RemoveBuildingSelection(this);
	}


	public void SetSelection(bool s){
		isSelected = s;
		Debug.Log ("Selected: "+isSelected);
		sprtR = GetComponent<SpriteRenderer> ();
		if (isSelected) {
			sprtR.color = Color.cyan;
		} else {
			sprtR.color = Color.white;
		}
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
