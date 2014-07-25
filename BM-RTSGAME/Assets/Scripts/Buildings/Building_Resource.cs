using UnityEngine;
using System.Collections;

public class Building_Resource : Building {

	GameObject guiObject;
	UserInterfaceGUI rui;
	public float resourceRate = 1f; //This is how many resources this building gives pr second.
	float temp = 0;
	float refreshUI = 0;
	bool isAddedToResource = false;

	// Use this for initialization
	void Start () {
		base.Start ();
		guiObject = GameObject.Find ("SpawnGUI");
		rui = guiObject.GetComponent<UserInterfaceGUI> ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
/*		if (isPlaced) {

			//Debug.Log("SHOULD ADD TO RESOURCE");
			temp += resourceRate*Time.deltaTime;
			//Debug.Log("RESOURCE: "+temp+" "+rui.ResourceAmpere);

			refreshUI += Time.deltaTime;
			if(refreshUI > 1f){
				rui.ResourceAmpere = (int)temp;
				refreshUI = 0;
			}

		}*/
	
		if (isPlaced && !isAddedToResource) {
			rui.ResourceAmpere += 1;
			isAddedToResource = true;
		}
	
	}


}
