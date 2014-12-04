using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]

public class SyncUnitScript : MonoBehaviour {
	
	private Vector3 ReferencePosition;
	
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
}
