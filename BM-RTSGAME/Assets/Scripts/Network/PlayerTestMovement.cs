using UnityEngine;
using System.Collections;

public class PlayerTestMovement : MonoBehaviour {

		public float speed = 10f;
		
		void Update()
		{
			// Makes sure that no other player can use the same controls.
			if (networkView.isMine)
			{
				InputMovement();
			}
		}
		
		void InputMovement()
		{	
				// Just simple character control
				if (Input.GetKey (KeyCode.W)) {
					transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, transform.position.y + speed, transform.position.z), Time.deltaTime * 5);
				}

				if (Input.GetKey (KeyCode.D)) {
					transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z), Time.deltaTime * 5);
				}

				if (Input.GetKey (KeyCode.A)) {
						transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z), Time.deltaTime * 5);
				}
		}
}
