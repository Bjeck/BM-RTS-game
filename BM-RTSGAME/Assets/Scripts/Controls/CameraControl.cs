using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Vector2 NavigationMargin = new Vector2 (100.0f, 100.0f);

	private Vector3 mousePos;
	private bool PlayerFound = false;

	private float Add_Up, Add_Down, Add_Left, Add_Right, Add_In, Add_Out;
	private int right_screen_side, left_screen_side, top_screen_side, bottom_screen_side;
	private float sizeOfCamera;

	public Vector2 ZoomConstraint = new Vector2(1.0f,30.0f);
	
	// Update is called once per frame
	void Update () {

		CheckForPlayer ();

		if(PlayerFound){

			// Resets the movement of camera, so it isnt effected if none of the function applies a difference.  
			UpdateInfo();

			// Update camera movement dependent on if the mouse is in the sides, or the scroolwheel is used.
			MouseNavigation();

			// Update camera movement dependent on if the keys are pressed.
			KeyboardNavigation();

			// Applies the changes to the camera. 
			ApplyNavigation();

		}
	}

	private void CheckForPlayer(){
		// Check whether there is a playerstash object in the scene
		if (!PlayerFound) {
			GameObject playerStashsh = GameObject.Find ("PlayerStash(Clone)");
			
			if (playerStashsh != null) {
				PlayerFound = true;
				print("Player Connected!");
			}
		}
	}

	private void UpdateInfo(){
		//////////////////////////////////////////////////////////////////////////////////////////
		////// UPDATE INFO /////////////////////////////////////////////////////////////////////// 
		//////////////////////////////////////////////////////////////////////////////////////////
		
		// RESET DIRECTION
		Add_Up = 0.0f;
		Add_Down = 0.0f;
		Add_Left = 0.0f;
		Add_Right = 0.0f;
		Add_In = 0.0f;
		Add_Out = 0.0f;
		
		right_screen_side = Screen.width;
		left_screen_side = 0;
		top_screen_side = Screen.height;
		bottom_screen_side = 0;
	}

	private void MouseNavigation(){

		//////////////////////////////////////////////////////////////////////////////////////////
		////// MOUSE NAVIGATION ////////////////////////////////////////////////////////////////// 
		//////////////////////////////////////////////////////////////////////////////////////////
		// FIND MOUSE POSITION
		
		mousePos = Input.mousePosition;
		bool mouseInsideScreen = false;
		
		// Inside the screen
		if(mousePos.y >= bottom_screen_side)
		{
			if(mousePos.y <= top_screen_side)
			{
				if(mousePos.x >= left_screen_side)
				{
					if(mousePos.x <= right_screen_side)
					{	
						mouseInsideScreen = true;		
					} 
				}
			}
		}
		
		if(mouseInsideScreen){
			
			// APPLY DIRECTION IN ZONES
			if(mousePos.x >= (right_screen_side - NavigationMargin.x)){		Add_Right += 5.0f;	}
			if(mousePos.x <= 0 + NavigationMargin.x){						Add_Right -= 5.0f;	}
			if(mousePos.y >= (top_screen_side - NavigationMargin.y)){		Add_Up += 5.0f;		}
			if(	mousePos.y <= 0 + NavigationMargin.y){						Add_Up -= 5.0f;		}
			
		}

		if(ZoomConstraint.x < GetComponent<Camera>().orthographicSize)
		{
			if (Input.GetAxis("Mouse ScrollWheel") < 0) 	{				Add_In -= 2;		}
		}
		if(ZoomConstraint.y > GetComponent<Camera>().orthographicSize)
		{
			if (Input.GetAxis("Mouse ScrollWheel") > 0) {					Add_In += 2; 		}
		}

		
		GetComponent<Camera>().orthographicSize += Add_In;
		sizeOfCamera = GetComponent<Camera>().orthographicSize;
	}

	private void KeyboardNavigation(){
		//////////////////////////////////////////////////////////////////////////////////////////
		////// KEYBOARD NAVIGATION /////////////////////////////////////////////////////////////// 
		//////////////////////////////////////////////////////////////////////////////////////////
		
		// APPLY DIRECTION
		if(Input.GetKey(KeyCode.UpArrow)){									Add_Up += 10.0f;	}
		if(Input.GetKey(KeyCode.DownArrow)){								Add_Down += 10.0f;	}
		if(Input.GetKey(KeyCode.LeftArrow)){								Add_Left += 10.0f; 	}
		if(Input.GetKey(KeyCode.RightArrow)){								Add_Right += 10.0f;	}
		

	}

	private void ApplyNavigation(){
		// FINALIZE DIRECTION
		Vector3 direction = new Vector3(Add_Right-Add_Left, Add_Up-Add_Down, 0.0f );
		
		// SET CAMERA POSITION
		transform.position = Vector3.Lerp(transform.position, transform.position + (direction * sizeOfCamera/2), Time.deltaTime); 
	}

}
