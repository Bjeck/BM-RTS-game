using UnityEngine;
using System.Collections;

public class PlayerInterface : MonoBehaviour {
	
	public Vector3 ReferencePosition;

	void Start(){

	}

	// Syncronizes Position
	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){

		if (stream.isWriting) {
			ReferencePosition = transform.position;
			stream.Serialize (ref ReferencePosition);

		} else {
			stream.Serialize (ref ReferencePosition);
			transform.position = ReferencePosition;

		}
	}

	// If not in range of anything, post as invisible
	public void MakeInvis(bool invis){

			if(invis){
				GetComponentInParent<PlayerInterface>().renderer.enabled = false;
			}
			
			else{
				GetComponentInParent<PlayerInterface>().renderer.enabled = true;
			}
	}
}
