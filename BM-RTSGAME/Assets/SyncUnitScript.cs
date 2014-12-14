using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]

public class SyncUnitScript : MonoBehaviour {
	
	private Vector3 EndPosition = new Vector3(0.0f,0.0f,0.0f); 
	private Vector3 StartPosition = new Vector3(0.0f,0.0f,0.0f); 

	private float syncDelay = 0.0f;
	private float syncTime = 0.0f;
	private float LastSyncTime = 0.0f;

	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){

		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		// If this script is the one sending something.
		if (stream.isWriting) {
			
			// Make a reference of the position right now.
			syncPosition = transform.position;
			stream.Serialize (ref syncPosition);
		
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);

		} 
		// If the script is the one receiving.
		else {
			
			// Receive the message from the sender. 
			stream.Serialize (ref syncPosition);
			stream.Serialize(ref syncVelocity);

			syncTime = 0.0f;
			syncDelay = Time.time - LastSyncTime;
			LastSyncTime = Time.time;

			StartPosition = transform.position;
			EndPosition = syncPosition + syncVelocity * syncDelay;
		}
	}

	void Update(){
		if(!networkView.isMine) {
			SyncMovement();		
		}
	}

	private void SyncMovement(){

		syncTime += Time.deltaTime;
		transform.position = Vector3.Lerp (StartPosition, EndPosition, syncTime / syncDelay);
	
	}

}
