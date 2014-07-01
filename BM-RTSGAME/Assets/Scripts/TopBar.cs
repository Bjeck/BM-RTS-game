using UnityEngine;
using System.Collections;

public class TopBar : MonoBehaviour {

	public int ResourceVoltage = 0; 
	public int ResourceAmpere = 0; 
	public GUIStyle TextSkin;
	public GUIStyle ResourceTextSkin;

	public Texture aTexture;

	private int pushVoltage = 43;
	private int pushAmpere = 120;

	void OnGUI() {

		if (!aTexture) {
			Debug.LogError("Assign a Texture in the inspector.");
			return;
		}

		GUI.DrawTexture(new Rect(0, 0, Screen.width, 35), aTexture);

		GUI.TextField(new Rect(10, 4, 80, 30), "V", TextSkin);
		GUI.TextField(new Rect(83, 4, 80, 30), "A", TextSkin);

		GUI.Box(new Rect(23, 4, 40, 15), "");
		GUI.Box(new Rect(100, 4, 40, 15), "");

		//-------------------- Easy fit of numbers in GUI
		if 		(	ResourceVoltage >= 1000	) 	{ GUI.Box(new Rect(pushVoltage-14, 	4, 22, 15), ""+ResourceVoltage, ResourceTextSkin);	}
		else if (	ResourceVoltage >= 100	) 	{ GUI.Box(new Rect(pushVoltage-7, 	4, 22, 15), ""+ResourceVoltage, ResourceTextSkin);	}
		else if (	ResourceVoltage >= 10	)	{ GUI.Box(new Rect(pushVoltage, 	4, 22, 15), ""+ResourceVoltage, ResourceTextSkin); 	}
		else 									{ GUI.Box(new Rect(pushVoltage+7, 	4, 22, 15), ""+ResourceVoltage, ResourceTextSkin); 	}

		if 		(	ResourceAmpere >= 1000	) 	{ GUI.Box(new Rect(pushAmpere-14, 	4, 22, 15), ""+ResourceAmpere, ResourceTextSkin);	}
		else if (	ResourceAmpere >= 100	) 	{ GUI.Box(new Rect(pushAmpere-7, 	4, 22, 15), ""+ResourceAmpere, ResourceTextSkin);	}
		else if (	ResourceAmpere >= 10	)	{ GUI.Box(new Rect(pushAmpere, 		4, 22, 15), ""+ResourceAmpere, ResourceTextSkin); 	}
		else 									{ GUI.Box(new Rect(pushAmpere+7, 	4, 22, 15), ""+ResourceAmpere, ResourceTextSkin); 	}

	}
}
