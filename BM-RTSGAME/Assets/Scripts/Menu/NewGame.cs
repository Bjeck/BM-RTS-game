using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {

	private bool MouseIsHovering = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (MouseIsHovering){
			transform.localScale = new Vector3(1.1f,1.1f,0.0f);
		}else{
			transform.localScale = new Vector3(1.0f,1.0f,0.0f);
		}

		if(Input.GetMouseButtonDown(0) && MouseIsHovering){
			Application.LoadLevel("FieldConstruction");
		}
	}

	void OnMouseOver(){
		MouseIsHovering = true;
	}
	void OnMouseExit(){
		MouseIsHovering = false;
	}
}
