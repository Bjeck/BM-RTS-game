using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mouse : MonoBehaviour {
	
	public List<Building> buildingsSelected = new List<Building>();
	public List<Unit> unitsSelected = new List<Unit>();

	public Texture2D selectionHighlight = null;
	public Rect selection = new Rect(0,0,0,0);
	public Vector3 startClick = -Vector3.one;


	// Use this for initialization
	void Start () {
	
	}

	//TO DO
	//double click/control click for selection of all units of same type.
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			startClick = Input.mousePosition;

			if(GUIUtility.hotControl == 0){ //Check if there is a GUI Element under the mouse. If not, continue with the Raycasting.
				Vector3 mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f)) { //check if anything is hit.

					LayerMask layermaskB = (1 << 10);
					LayerMask layermaskU = (1 << 12);
					if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f, layermaskB)) {	//check if it is a building that was clicked on
						Debug.Log ("CLICKED ON BUILDING " + hit.transform.name); 
					
						if (!ShiftKeyDown ()){ //allowing multiple units and buildings to be selected if shift is held. And deselects other things if shift is not held.
							ClearBuildingSelections ();
							ClearUnitSelections();
						}

						Building buildingScript = hit.transform.GetComponent<Building> ();

						if(ShiftKeyDown() && buildingsSelected.Count > 0 && buildingScript.isSelected){ //if shift, and the building is already selected, deselect it
							RemoveBuildingSelection(buildingScript);
						}
						else{
							AddBuildingSelection(buildingScript); //otherwise, select it
						}

					}

					else if(Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f, layermaskU)){//check if it is a unit that was clicked on
						Debug.Log ("CLICKED ON UNIT " + hit.transform.name);

						if (!ShiftKeyDown ()){ //allowing multiple units and buildings to be selected if shift is held.
							ClearBuildingSelections ();
							ClearUnitSelections();
						}

						Unit unitScript = hit.transform.GetComponent<Unit> (); //if so, say it is selected.

						if(ShiftKeyDown() && unitsSelected.Count > 0 && unitScript.isSelected){ //if shift, and the building is already selected, deselect it
							RemoveUnitSelection(unitScript);
						}
						else{
							AddUnitSelection(unitScript); //otherwise, select it
						}


					}

					else { //if nothing was clicked on. Deselect everything.
						if(!ShiftKeyDown()) {
							ClearBuildingSelections ();
							ClearUnitSelections();
						}
					}

				}
			}
		}

		else if(Input.GetMouseButtonUp(0)){ //resetting selection rectangle if mousebutton is released
			startClick = -Vector3.one;
		}

		if (Input.GetMouseButton (0)) { //Creating selection rectangle
			selection = new Rect(startClick.x, InvertMouseY(startClick.y),Input.mousePosition.x - startClick.x, InvertMouseY(Input.mousePosition.y) - InvertMouseY(startClick.y));	

			if(selection.width < 0){
				selection.x += selection.width;
				selection.width = -selection.width;
			}
			if(selection.height < 0){
				selection.y += selection.height;
				selection.height = -selection.height;
			}

		}

	}



// ---- ADDING AND REMOVING

	public void AddBuildingSelection(Building buildingScript){
		buildingScript.SetSelection (true);
		buildingsSelected.Add (buildingScript);
	}

	public void AddUnitSelection(Unit unitScript){
		unitScript.SetSelection (true);
		unitsSelected.Add (unitScript);
	}

	public void RemoveUnitSelection(Unit unitScript){
		unitScript.SetSelection (false);
		unitsSelected.Remove (unitScript);
	}

	public void RemoveBuildingSelection(Building buildingScript){
		buildingScript.SetSelection (false);
		buildingsSelected.Remove (buildingScript);
	}


	public bool CheckUnitInList(Unit unit){ //Checks if parameter unit is already in the unitList. NEED TO ADD ONE FOR BUILDINGS LATER
		foreach (Unit u in unitsSelected) {
			if(u == unit){
				return true;
			}
		}
		return false;
	}
	

	public bool ShiftKeyDown(){
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			return true;		
		} else
			return false;
	}


	
//------- CLEARING

	public void ClearBuildingSelections(){ //CLEAR BUILDING LIST
		if(buildingsSelected.Count > 0)
			Debug.Log (buildingsSelected [0]);

		foreach (Building b in buildingsSelected) {
			b.SetSelection(false);
		}
		buildingsSelected.Clear ();
	}

	public void ClearUnitSelections(){ //CLEAR UNIT LIST
		if(unitsSelected.Count > 0)
			Debug.Log (unitsSelected [0]);
		
		foreach (Unit u in unitsSelected) {
			u.SetSelection(false);
		}
		unitsSelected.Clear ();
	}



// -------- SELECTION BOX

	private void OnGUI(){ //drawing the selection box on screen.
		if (startClick != -Vector3.one) {
			GUI.color = new Color(1,1,1,0.5f);
			GUI.DrawTexture(selection,selectionHighlight);
		}
	}
	
	public bool isDrawingBox(){ //checks if a box is being drawed. Used for individual selection of things
		if(selection.size.y < 3 && selection.size.x < 3)
			return false;
		else
			return true;
	}
	 
	public static float InvertMouseY(float y){ //just a dumb thing that the selection box needs.
		return Screen.height - y;
	}
}
