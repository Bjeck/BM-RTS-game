using UnityEngine;
using System.Collections;

public class UnitCollisionDetection : MonoBehaviour {

	[SerializeField]
	/// <summary>
	/// The description of script.
	/// </summary>
	private string DescriptionOfScript = "THE VIEW DISTANCE TRIGGER THAT CONTROLS FOG.";

	/// <summary>
	/// Variable storing the information of view radius in loading properties.
	/// </summary>
	public int ViewDistanceZeroForStandard;

	/// <summary>
	/// The trigger counter storing how many enters the trigger radius.
	/// </summary>
	private int triggerCounter = 0;

	void Start(){

		// Assign radius, if it isnt changed. 
		AssignRadius ();
	}

	/// <summary>
	/// If the radius is 0, this function automatically assigns the basic property from Loading Properties. If other than 0, nothing happens, and therefore it can be customized.   
	/// </summary>
	private void AssignRadius(){

		// If the radius havent been changed. 
		if(ViewDistanceZeroForStandard == 0){

			// Find the basic setting from "LoadingProperties".
			ViewDistanceZeroForStandard = GameObject.Find ("NetworkManager").GetComponent<LoadingProperties>().ViewDistance;

			// And apply it as the radius in the circle collider. 
			GetComponent<CircleCollider2D> ().radius = ViewDistanceZeroForStandard;
		}
	}

	/// <summary>
	/// The view radius or "sight" radius of the unit. If your opponent unit collides with one of your units, it makes its parent visible to you. 
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerEnter2D(Collider2D other){

		// If the networkview is not mine. The owner of the unit should not interphere with its own units visibility. 
		if (!GetComponentInParent<NetworkView> ().isMine) {

			// If the object is a player
			if (other.gameObject.tag == "Player") {

				// If the other gameobject belongs to me.
				if (other.gameObject.networkView.isMine) {

					// Add one - Count the units inside the radius
					triggerCounter++;

					// Only make visible when there is one. Avoids making the call when there is just several units inside the view radius.
					if(triggerCounter == 1){
						GetComponentInParent<PlayerInterface> ().MakeInvis (false);
					}
				}
			} 
		}
	}

	/// <summary>
	/// The view radius or "sight" radius of the unit. If your opponent unit exits collision with all of your units, it makes its parent invisible to you.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerExit2D(Collider2D other){

		// If the networkview is not mine. The owner of the unit should not interphere with its own units visibility. 
		if(!GetComponentInParent<NetworkView>().isMine){

			// If the object is a player
			if (other.gameObject.tag == "Player") {

				// If the other gameobject belongs to me.
				if (other.gameObject.networkView.isMine){

					// Remove one - Count the units inside the radius
					triggerCounter--;

					// Only make ivisible when there is none inside the raidus.
					if(triggerCounter == 0){
						GetComponentInParent<PlayerInterface> ().MakeInvis (true);
					}

				}
			} 
		}
	}
}
