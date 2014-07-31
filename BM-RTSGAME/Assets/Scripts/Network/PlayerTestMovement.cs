using UnityEngine;
using System.Collections;

public class PlayerTestMovement : MonoBehaviour {

	public float speed = 10f;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector2 syncStartPosition = Vector2.zero;
	private Vector2 syncEndPosition = Vector2.zero;


	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){

		Vector3 syncPosition = Vector3.zero;

		if (stream.isWriting) {

			syncPosition = transform.position;
			stream.Serialize(ref syncPosition);

		} else {
			stream.Serialize(ref syncPosition);

			syncTime = 0.0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;

			Vector2 convertTo2D = new Vector2(syncPosition.x, syncPosition.y);
			syncStartPosition = transform.position;
			syncEndPosition = convertTo2D;
		}
	}


	void Update()
	{
		// Makes sure that no other player can use the same controls.
		if (networkView.isMine)
		{
			InputMovement();
		} else {
			SyncedMovement();
		}
	}
	
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		transform.position = Vector2.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}
	
	void InputMovement()
	{	
			// Just simple character control
			if (Input.GetKey (KeyCode.W)) {
				transform.position = Vector2.Lerp (transform.position, new Vector2 (transform.position.x, transform.position.y + speed), Time.deltaTime * 5);
			}

			if (Input.GetKey (KeyCode.D)) {
				transform.position = Vector2.Lerp (transform.position, new Vector2 (transform.position.x + speed, transform.position.y), Time.deltaTime * 5);
			}

			if (Input.GetKey (KeyCode.A)) {
					transform.position = Vector2.Lerp (transform.position, new Vector2 (transform.position.x - speed, transform.position.y), Time.deltaTime * 5);
			}
	}


}
