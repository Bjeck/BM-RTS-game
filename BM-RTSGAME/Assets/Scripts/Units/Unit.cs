using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public bool isSelected = false;
	bool underConstruction = true;
	public SpriteRenderer sprtR;
	Mouse mouseScript;

	// Use this for initialization
	public void Start () {
		mouseScript = GameObject.Find("Main Camera").GetComponent<Mouse> ();
	}
	
	// Update is called once per frame
	public void Update () {
		if (renderer.isVisible && Input.GetMouseButton (0) && mouseScript.isDrawingBox()) { //for selection box. makes itself selection if it is inside the box.
			Vector3 camPos = Camera.main.WorldToScreenPoint (transform.position);
			camPos.y = Mouse.InvertMouseY (camPos.y);
			if(mouseScript.selection.Contains (camPos))
				mouseScript.AddUnitSelection(this);
			if(!mouseScript.selection.Contains (camPos) && !mouseScript.ShiftKeyDown())
				mouseScript.RemoveUnitSelection(this);
		}
	}

	public void SetSelection(bool s){
		isSelected = s;
		//Debug.Log ("Selected: "+isSelected);
		sprtR = GetComponent<SpriteRenderer> ();
		if (isSelected) {
			sprtR.color = Color.cyan;
		} else {
			sprtR.color = Color.white;
		}
	}

	public bool GetSelection(){
		return isSelected;
	}
}
