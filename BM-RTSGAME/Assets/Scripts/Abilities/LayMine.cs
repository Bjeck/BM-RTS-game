using UnityEngine;
using System.Collections;

public class LayMine : DirectTarget {

	GameObject mineObject;

	// Use this for initialization
	void Start () {
		base.Start ();

		//targetCursor = base.targetCursor;
		//targetCursorGreen = base.targetCursorRed;
		//targetCursorRed = base.targetCursorRed;

	//	Debug.Log (targetCursorRed);

		keyToUse = KeyCode.N;
		cost = 80;
		range = 10f;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		base.Update ();
		
		if (isTargeting) {
			if (Input.GetMouseButtonDown (0)) {
				//Debug.Log("MOUSE CLICK!");
				
				if (caster.GetComponent<Unit> ().memory - cost < 0) {
					Debug.Log("NOT ENOUGH memory to cast");
					return;
				}
				
				if(dist > range){
					//Debug.Log("OUT OF RANGE!");
					return;
				}
				if (GUIUtility.hotControl == 0) { //Check if there is a GUI Element under the mouse. If not, continue with the Raycasting.
					//Debug.Log("NO UI");
					Vector3 mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					RaycastHit hit;
					//LayerMask layermaskU = (1 << 12);
					if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f)) {
					//	Debug.Log("LAYING MINE");
						laymine(mPos);
					}
				}
			}
		}




	}


	void OnGUI(){
		GUI.skin = guiScript.guiSkin;
		
		if(caster.GetComponent<Unit>().isSelected){
			float[] coord = guiScript.GetButtonCoordinates (1,0);
			
			//Debug.Log("FROM BUILDING! "+guiScript.GetButtonCoordinates ()[0]+ " "+coord[0]);
			if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "MINE", guiStyle)){
				Do ();
			}
		}
	}



	void laymine(Vector3 pos){
		base.Cast ();
		mineObject = (GameObject)Instantiate(Resources.Load("Mine",typeof(GameObject)));
		mineObject.GetComponent<MineScript> ().player1 = caster.GetComponent<Unit>().player1;
		pos.z = -1;
		mineObject.transform.position = pos;
	}


}
