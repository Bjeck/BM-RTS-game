using UnityEngine;
using System.Collections;

public class PlayerInterface : MonoBehaviour {

	[SerializeField]
	/// <summary>
	/// The description of script.
	/// </summary>
	private string DescriptionOfScript = "THE INTERFACE OF UNIT PROPERTIES.";

	/// <summary>
	/// The position that is referenced from this object to the object on the opponents computer.
	/// </summary>
	private Vector3 ReferencePosition;

	/// <summary>
	/// Syncronizes the position of this object with the object on the opponents hierachy. 
	/// </summary>
	/// <param name="stream">Stream.</param>
	/// <param name="info">Info.</param>
	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){

		// If this script is the one sending something.
		if (stream.isWriting) {

			// Make a reference of the position right now.
			ReferencePosition = transform.position;

			// Send it through the stream.
			stream.Serialize (ref ReferencePosition);

		} 
		// If the script is the one receiving.
		else {

			// Receive the message from the sender. 
			stream.Serialize (ref ReferencePosition);

			// Assigns the value to this object's position. 
			transform.position = ReferencePosition;

		}
	}

	/// <summary>
	/// Makes this unit invisible with the boolean message true, or visible with the boolean message false.
	/// </summary>
	/// <param name="invis">If set to <c>true</c> invis.</param>
	public void MakeInvis(bool invis){

		// If the caller wants the unit to become visibile the mesh renderer is enabled.
		if(invis){
			GetComponentInParent<PlayerInterface>().renderer.enabled = false;
		}

		// If the caller wants the unit to become ivisibile the mesh renderer is disabled.
		else{
			GetComponentInParent<PlayerInterface>().renderer.enabled = true;
		}
	}
}
