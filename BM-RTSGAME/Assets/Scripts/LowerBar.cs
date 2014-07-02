using UnityEngine;
using System.Collections;

public class LowerBar : MonoBehaviour {

	public GUIStyle BarGuiStyle;
	public Texture BarTexture;

	void OnGUI() {
		GUI.DrawTexture(new Rect(0, Screen.height-250, Screen.width, 250), BarTexture);

		GUI.Button (new Rect(15, 450, 155, 130), "MINI-MAP");

		GUI.Button (new Rect(782, 450, 58, 41), "A1");
		GUI.Button (new Rect(844, 450, 58, 41), "A2");
		GUI.Button (new Rect(906, 450, 58, 41), "A3");

		GUI.Button (new Rect(782, 495, 58, 41), "A4");
		GUI.Button (new Rect(844, 495, 58, 41), "A5");
		GUI.Button (new Rect(906, 495, 58, 41), "A6");

		GUI.Button (new Rect(782, 542, 58, 41), "A7");
		GUI.Button (new Rect(844, 542, 58, 41), "A8");
		GUI.Button (new Rect(906, 542, 58, 41), "A9");

	}
}
