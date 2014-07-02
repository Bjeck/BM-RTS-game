using UnityEngine;
using System.Collections;

public class UserInterfaceGUI : MonoBehaviour {

	public int ResourceVoltage = 0; 
	public int ResourceAmpere = 0; 
	public GUIStyle TextSkin;
	public GUIStyle ResourceTextSkin;
	public GUIStyle BottomBarGuiStyle;
	public Texture BottomBarTexture;
	public Texture TopBarTexture;

	private float ProportionWidth;
	private float ProportionHeight;
	private Vector3 scale;

	void OnGUI() {

		//------------------------------------------------------------------------ IF NOT TEXTURE IS ADDED
		if (!TopBarTexture) {
			Debug.LogError("Assign a Texture in the inspector.");
			return;
		}

		//------------------------------------------------------------------------ Setting Aspect Ratio
		ProportionWidth = Screen.width/16;
		ProportionHeight = Screen.height/9;

		//------------------------------------------------------------------------ Draw Top GUI Controls
		// Setting fontsize in proportion to resolution
		TextSkin.fontSize = (int)(0.2f*ProportionHeight); 
		ResourceTextSkin.fontSize = (int)(0.2f*ProportionHeight); 

		// DRAWING: Background top bar image
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/9), TopBarTexture);

		// DRAWING: Resource Symbols
		GUI.Box(new Rect(0.28f*ProportionWidth, 0.06f*ProportionHeight, 22, 15), "V", TextSkin);
		GUI.Box(new Rect(2.18f*ProportionWidth, 0.06f*ProportionHeight, 22, 15), "A", TextSkin);

		// DRAWING: Resource Amount
		GUI.Box(new Rect(1.7f*ProportionWidth, 0.06f*ProportionHeight, 22, 15), ""+ResourceVoltage, ResourceTextSkin);
		GUI.Box(new Rect(3.6f*ProportionWidth, 0.06f*ProportionHeight, 22, 15), ""+ResourceAmpere, ResourceTextSkin);

		//----------------------------------------------------------------------------------------------------------------

		//------------------------------------------------------------------------ Draw Bottom GUI Controls
		// DRAWING: Background top bar image
		GUI.DrawTexture(new Rect(0, Screen.height-Screen.height/2.5f, Screen.width, Screen.height/2.5f), BottomBarTexture);

		// DRAWING: MiniMap
		GUI.Button (new Rect(0.28f*ProportionWidth, 7.0f*ProportionHeight, 2.5f*ProportionWidth, 1.80f*ProportionHeight), "MINI-MAP");

		// DRAWING: Array of Abilities
		for(int i = 0; i<3; i++){
			for(int j = 0; j<3; j++){
				if(GUI.Button (new Rect((12.8f+(1.03f*i))*ProportionWidth, (6.95f+(0.65f*j))*ProportionHeight, 0.95f*ProportionWidth, 0.6f*ProportionHeight), ""+i+":"+j)) {		
					print (""+i+":"+j);	
				}
			}
		}

	}

}
