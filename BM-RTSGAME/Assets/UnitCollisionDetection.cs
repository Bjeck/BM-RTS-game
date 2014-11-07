using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitCollisionDetection : MonoBehaviour {

	public int ViewRadius_AssignedInNetworkManager;
	
	private int triggerCounter = 0;

	void Start(){
		// FIND THE LOADING PROPERTIES AND ASSIGNING THE VIEWDISTANCE AS RADIUS TO THE COLLIDER
		ViewRadius_AssignedInNetworkManager = GameObject.Find ("NetworkManager").GetComponent<LoadingProperties>().ViewDistance;
		GetComponent<CircleCollider2D> ().radius = ViewRadius_AssignedInNetworkManager;

	}

	void OnTriggerEnter2D(Collider2D other){

		if (GetComponentInParent<NetworkView> ().isMine) {
			if (other.gameObject.tag == "Player") {

					if (!other.gameObject.networkView.isMine) {
						triggerCounter++;
						print (triggerCounter);

						if(triggerCounter == 1){
							GetComponentInParent<PlayerInterface> ().MakeInvis (false);
						}
					}
			} 
		}
	}

	void OnTriggerExit2D(Collider2D other){

		if(GetComponentInParent<NetworkView>().isMine){
			if (other.gameObject.tag == "Player") {

				if (!other.gameObject.networkView.isMine){
					triggerCounter--;
					print (triggerCounter);
					if(triggerCounter == 0){
						GetComponentInParent<PlayerInterface> ().MakeInvis (true);
					}

				}
			} 
		}
	}
}
