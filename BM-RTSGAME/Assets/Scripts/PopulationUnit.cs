using UnityEngine;
using System.Collections;

public class PopulationUnit : MonoBehaviour {

	private bool activateUpdate = false;

	void Awake(){
		activateUpdate = true;
	}


	void Update(){
		if(GameObject.Find ("BuildingManager").GetComponent<BuildingManager>().isDragging == false && activateUpdate){
			GameObject.Find ("BuildingManager").GetComponent<BuildingManager> ().PopulationPlayer1 += 1;
			activateUpdate = false;
		}
	}
		

}
