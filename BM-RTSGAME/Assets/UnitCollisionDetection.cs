using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitCollisionDetection : MonoBehaviour {


	public int ViewRadius_AssignedInNetworkManager;

	private List<string> ListOfTriggers = new List<string>();
	
	void Start(){
		ViewRadius_AssignedInNetworkManager = GameObject.Find ("NetworkManager").GetComponent<LoadingProperties>().ViewDistance;
		GetComponent<CircleCollider2D> ().radius = ViewRadius_AssignedInNetworkManager;
	}

	void OnTriggerEnter2D(Collider2D other){

		// IF PLAYER
		if(other.gameObject.tag == "Player"){

			// MAKE TO STRING
			string otherID = other.GetComponent<NetworkView> ().viewID.ToString ();

			// ADDING TO TRIGGER LIST
			ListOfTriggers.Add(otherID);

			// MAKE VISIBLE
			GetComponentInParent<PlayerInterface>().MakeInvis(false);

		}
		for (int i = 0; i<ListOfTriggers.Count; i++) {
			print(ListOfTriggers[i]);	
		}
	}

	void OnTriggerExit2D(Collider2D other){

		// IF PLAYER
		if (other.gameObject.tag == "Player") {

			// MAKE TO STRING
			string otherID = other.GetComponent<NetworkView> ().viewID.ToString ();

			// IF NO ITEMS
			if(ListOfTriggers.Count > 0){

				// CHECK LIST
				for (int i = 0; i<ListOfTriggers.Count; i++){

					// IF ID IS IN LIST
					if(ListOfTriggers[i] == otherID ){

						ListOfTriggers.Remove(otherID);

						// MAKE INVIS
						GetComponentInParent<PlayerInterface> ().MakeInvis (true);
					}
				}
			} 

		}


	}
}
