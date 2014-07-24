using UnityEngine;
using System.Collections;

public enum SideScrolling {	None, Tilt, Pan, PanAndTilt }

public class UserControlSwitches : MonoBehaviour {

	public SideScrolling DisableSideControl;

	// Use this for initialization
	void Start () {

		//----------------------------------------------------------------- DISABLE TILT / PETER
		GameObject disableThis = GameObject.Find ("Main Camera"); 
		string DisableChoice = DisableSideControl.ToString();
		// TILT
		if (DisableChoice == "Tilt") { 
			disableThis.GetComponent<CameraControl>().TiltPanDisable = 'P'; 
		}

		// PAN
		if (DisableChoice == "Pan"){  
			disableThis.GetComponent<CameraControl>().TiltPanDisable = 'T';
		}

		// PAN AND TILT
		if (DisableChoice == "PanAndTilt"){ 
			disableThis.GetComponent<CameraControl>().TiltPanDisable = 'A';
		}

		// NONE
		if (DisableChoice == "None"){ 
			disableThis.GetComponent<CameraControl>().TiltPanDisable = 'N';
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
