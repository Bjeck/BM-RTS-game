using UnityEngine;
using System.Collections;

public class TopBar : MonoBehaviour {

	public int ResourceVoltage = 0; 
	public int ResourceAmpere = 0; 
	public GUIStyle TextSkin;
	public GUIStyle ResourceTextSkin;

	public Texture aTexture;

	private int pushVoltage = 171;
	private int pushAmpere = 372;

	void OnGUI() {

		// NON dynamic 
		if (!aTexture) {
			Debug.LogError("Assign a Texture in the inspector.");
			return;
		}

		GUI.DrawTexture(new Rect(0, 0, Screen.width, 50), aTexture);

		GUI.TextField(new Rect(33, 3, 80, 30), "V", TextSkin);
		GUI.TextField(new Rect(231, 3, 80, 30), "A", TextSkin);

		//-------------------- Easy fit of numbers in GUI
		if 		(	ResourceVoltage >= 1000	) 	{ GUI.Box(new Rect(pushVoltage-20, 	3, 22, 15), ""+ResourceVoltage, ResourceTextSkin);	}
		else if (	ResourceVoltage >= 100	) 	{ GUI.Box(new Rect(pushVoltage-10, 	3, 22, 15), ""+ResourceVoltage, ResourceTextSkin);	}
		else if (	ResourceVoltage >= 10	)	{ GUI.Box(new Rect(pushVoltage, 	3, 22, 15), ""+ResourceVoltage, ResourceTextSkin); 	}
		else 									{ GUI.Box(new Rect(pushVoltage+10, 	3, 22, 15), ""+ResourceVoltage, ResourceTextSkin); 	}

		if 		(	ResourceAmpere >= 1000	) 	{ GUI.Box(new Rect(pushAmpere-20, 	3, 22, 15), ""+ResourceAmpere, ResourceTextSkin);	}
		else if (	ResourceAmpere >= 100	) 	{ GUI.Box(new Rect(pushAmpere-10, 	3, 22, 15), ""+ResourceAmpere, ResourceTextSkin);	}
		else if (	ResourceAmpere >= 10	)	{ GUI.Box(new Rect(pushAmpere, 		3, 22, 15), ""+ResourceAmpere, ResourceTextSkin); 	}
		else 									{ GUI.Box(new Rect(pushAmpere+10, 	3, 22, 15), ""+ResourceAmpere, ResourceTextSkin); 	}

	}

	void Update(){

		if (Input.GetKeyDown(KeyCode.Space)){
			ResourceVoltage+= 10;
			ResourceAmpere+= 10;
		}
	}

}
