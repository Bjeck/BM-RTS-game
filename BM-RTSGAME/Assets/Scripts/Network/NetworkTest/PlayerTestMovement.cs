using UnityEngine;
using System.Collections;

public class PlayerTestMovement : MonoBehaviour {

	void Update()
	{
		if (networkView.isMine) {

			if (Input.GetKey (KeyCode.G)) {
					transform.position = new Vector3 (0.0f, 1.0f, 0.0f);	
			}

			if (networkView.isMine) {
					if (Input.GetKey (KeyCode.W)) {
							networkView.RPC ("Move_Up", RPCMode.AllBuffered, null);
					}
					if (Input.GetKey (KeyCode.S)) {
							networkView.RPC ("Move_Down", RPCMode.AllBuffered, null);
					}
					if (Input.GetKey (KeyCode.A)) {
							networkView.RPC ("Move_Left", RPCMode.AllBuffered, null);
					}
					if (Input.GetKey (KeyCode.D)) {
							networkView.RPC ("Move_Right", RPCMode.AllBuffered, null);
					}
			}
		}
	}




	[RPC]
	public void Move_Up(){
		transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0.0f,1.0f,0.0f), Time.deltaTime * 5);
	}

	[RPC]
	public void Move_Down(){
		transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0.0f,-1.0f,0.0f), Time.deltaTime * 5);
	}

	[RPC]
	public void Move_Left(){
		transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-1.0f,0.0f,0.0f), Time.deltaTime * 5);
	}

	[RPC]
	public void Move_Right(){
		transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(1.0f,0.0f,0.0f), Time.deltaTime * 5);
	}

}
