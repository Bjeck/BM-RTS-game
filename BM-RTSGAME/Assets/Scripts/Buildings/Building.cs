using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

	GameObject buildMan;
	public BuildingManager bManScript;
	public UserInterfaceGUI guiScript;
	public GUIStyle guiStyle;
	public bool isSelected = false;
	public bool isPlaced = false;
	public bool isBuilding = false;
	public bool isConstructing = false;
	public SpriteRenderer sprtR;
	public Light buildingLight;
	Mouse mouseScript;
	public bool player1 = false;
	public int health = 100;
	public bool isUnitBuilding = false;
	public Vector3 Waypoint;
	public GameObject wayPointMarker;

	private string nameOfCollision;

	// Use this for initialization
	public void Start () {
		//Debug.Log ("BUILDING HERE!");
		buildMan = GameObject.Find ("BuildingManager");
		bManScript = buildMan.GetComponent<BuildingManager> ();
		mouseScript = GameObject.Find("Main Camera").GetComponent<Mouse> ();
		player1 = bManScript.player1;
		guiScript = GameObject.Find ("SpawnGUI").GetComponent<UserInterfaceGUI> ();
		guiStyle = guiScript.guiStyle;
	}
	
	// Update is called once per frame
	public void Update () {

		if (health <= 0) {
			Die ();
			return;
		}
	
		if (renderer.isVisible && Input.GetMouseButton (0) && mouseScript.isDrawingBox() && !bManScript.isDragging) { //for selection box. makes itself selection if it is inside the box.
			Vector3 camPos = Camera.main.WorldToScreenPoint (transform.position);
			camPos.y = Mouse.InvertMouseY (camPos.y);
			if(mouseScript.selection.Contains (camPos) && !mouseScript.CheckBuildingInList(this))
				mouseScript.AddBuildingSelection(this);
			if(!mouseScript.selection.Contains (camPos) && !mouseScript.ShiftKeyDown())
				mouseScript.RemoveBuildingSelection(this);
		} 
		if (mouseScript.unitsSelected.Count > 0) //if any unit in selection box, don't select buildings.
			mouseScript.RemoveBuildingSelection(this);
	}


	public void SetSelection(bool s){
		isSelected = s;
		//Debug.Log ("Selected: "+isSelected);
		sprtR = GetComponent<SpriteRenderer> ();
		if (isSelected) {
			sprtR.color = Color.cyan;
			if(isUnitBuilding){
				//Debug.Log ("unit prod selection is selected");
				wayPointMarker = (GameObject)Instantiate(Resources.Load("Effects/WaypointMarker",typeof(GameObject)));
				wayPointMarker.transform.position = Waypoint;
			}
		} else {
			sprtR.color = Color.white;
			if(wayPointMarker != null)
				Destroy(wayPointMarker);
		}
	}


	//FOR PLACEMENT
	void OnTriggerEnter(Collider c){
		if(!isPlaced){
			if (c.tag == "Building" || c.tag == "Obstacle") {
				bManScript.colliders.Add(c);
			}else if(c.tag == "Resource"){
				//Debug.Log("THERE'S A RESOURCE");
				bManScript.resources.Add(c);
			}
		}
	}
	
	void OnTriggerExit(Collider c){
		if(!isPlaced){
			if (c.tag == "Building" || c.tag == "Obstacle") {
				bManScript.colliders.Remove(c);	
			}else if(c.tag == "Resource"){
				//Debug.Log("REMOVED A RESOURCE");
				bManScript.resources.Remove(c);
			}
		}
	}

	public void Die(){ //The unit dies. We should probably add some explosion effects or something cool :D
		mouseScript.RemoveBuildingSelection(this);
		Destroy (this.gameObject);
	}
}
