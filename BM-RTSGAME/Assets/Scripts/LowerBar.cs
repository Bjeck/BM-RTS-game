using UnityEngine;
using System.Collections;

public class LowerBar : MonoBehaviour {

	public GUIStyle BarGuiStyle;
	public Texture BarTexture;

	void OnGUI() {
		GUI.DrawTexture(new Rect(0, Screen.height-250, Screen.width, 250), BarTexture);

		GUI.Button (new Rect(15, 450, 155, 130), "MINI-MAP");

		//-------------------- Button 0,0
		if(GUI.Button (new Rect(782, 450, 58, 41), "A1")){
			print ("0,0 was pressed");
		}

		if(GUI.Button (new Rect(844, 450, 58, 41), "A2")){
			print ("1,0 was pressed");
		}

		if(GUI.Button (new Rect(906, 450, 58, 41), "A3")){
			print ("2,0 was pressed");
		}

		if(GUI.Button (new Rect(782, 495, 58, 41), "A4")){
			print ("0,1 was pressed");
		}

		if(GUI.Button (new Rect(844, 495, 58, 41), "A5")){
			print ("1,1 was pressed");
		}

		if(GUI.Button (new Rect(906, 495, 58, 41), "A6")){
			print ("2,1 was pressed");
		}

		if(GUI.Button (new Rect(782, 542, 58, 41), "A7")){
			print ("0,2 was pressed");
		}

		if(GUI.Button (new Rect(844, 542, 58, 41), "A8")){
			print ("1,2 was pressed");
		}

		if(GUI.Button (new Rect(906, 542, 58, 41), "A9")){
			print ("2,2 was pressed");
		}

	}
}
