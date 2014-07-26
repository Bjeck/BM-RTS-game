using UnityEngine;
using System.Collections;

public class UserInterfaceGUI : MonoBehaviour {

	//------------------------------- PUBLIC
	public int ResourceAmpere = 0; 
	public int ResourceWatt = 0;
	public GUIStyle TextSkin;
	public GUIStyle ResourceTextSkin;
	public GUIStyle BottomBarGuiStyle;
	public Texture BottomBarTexture;
	public Texture TopBarTexture;

	//------------------------------- PRIVATE
	private float ProportionWidth;
	private float ProportionHeight;
	private Vector3 scale;

	//------------------------------- OTHER
	int ResourceVoltage = 5; 
	int wattAddition = 0;
	float refreshUI = 0;
	float temp = 0;
	float voltageRate = 5;
	float voltageDecreaseRate = 20;
	bool shouldDecreaseVolt = true;

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
		GUI.Box(new Rect(4.18f*ProportionWidth, 0.06f*ProportionHeight, 22, 15), "W", TextSkin);


		// DRAWING: Resource Amount
		GUI.Box(new Rect(1.7f*ProportionWidth, 0.06f*ProportionHeight, 22, 15), ""+ResourceVoltage, ResourceTextSkin);
		GUI.Box(new Rect(3.6f*ProportionWidth, 0.06f*ProportionHeight, 22, 15), ""+ResourceAmpere, ResourceTextSkin);
		GUI.Box(new Rect(5.6f*ProportionWidth, 0.06f*ProportionHeight, 22, 15), ""+ResourceWatt, ResourceTextSkin);

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


	void Update () { 
		if (shouldDecreaseVolt) {
			StartCoroutine(voltageCounterDecrease());
			shouldDecreaseVolt = false;
		}

		//Calculating and updating Watts
		refreshUI += Time.deltaTime;
		if(refreshUI > 1f){
			wattAddition = ResourceAmpere * ResourceVoltage;
			//Debug.Log(wattAddition);
			ResourceWatt += wattAddition;
			refreshUI = 0;
		}


	}

	
	IEnumerator voltageCounterDecrease(){
		float t = 0;
		while(t<voltageDecreaseRate){
			t+=Time.deltaTime;
			yield return 0;
		}
		ResourceVoltage--;
		shouldDecreaseVolt = true;

		if(ResourceVoltage == 1){
			shouldDecreaseVolt = false;
		}

		yield return 0;
	}



}
