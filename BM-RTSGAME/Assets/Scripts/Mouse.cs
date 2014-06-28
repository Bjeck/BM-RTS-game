using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mouse : MonoBehaviour {

	bool isAnySelection = false;
	List<Building> buildingsSelected = new List<Building>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) { 
			if(GUIUtility.hotControl == 0){ //Check if there is a GUI Element under the mouse. If not, continue with the Raycasting.
				Vector3 mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f)) { //check if anything is hit.
					Debug.Log ("YOU CLICKED ON SOMETHING!");

					//find out if it is a building that was clicked on

					LayerMask layermask = (1 << 10);
					if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f, layermask)) {
						Debug.Log ("HIT BUILDING " + hit.transform.name); //if so, say it is selected.

						if (!ShiftKeyDown ()) //allowing multiple buildings to be selected if shift is held.
								ClearAllSelections ();

						Building buildingScript = hit.transform.GetComponent<Building> ();
						buildingScript.SetSelection (true);
						buildingsSelected.Add (buildingScript);
						isAnySelection = true;
						} 										//we prolly gonna add units here later?
						else {
						isAnySelection = false;
						ClearAllSelections ();
					}

				}
			}
			else
				return;
		}
	}

	public void ClearAllSelections(){
		if(buildingsSelected.Count > 0)
			Debug.Log (buildingsSelected [0]);

		foreach (Building b in buildingsSelected) {
			b.SetSelection(false);
		}
		buildingsSelected.Clear ();
	}


	public bool ShiftKeyDown(){
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			return true;		
		} else
			return false;
	}
}
