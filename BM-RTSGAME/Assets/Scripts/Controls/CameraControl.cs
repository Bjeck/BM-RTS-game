using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public int NavigationMarginTopAndBot = 0;
	public int NavigationMarginLeftAndRight = 0;

	private Vector3 mousePos;
	private bool PlayerFound = false;

	private float Add_Up, Add_Down, Add_Left, Add_Right, Add_In, Add_Out;
	private int right_screen_side, left_screen_side, top_screen_side, bottom_screen_side;
	
	// Update is called once per frame
	void Update () {

		// Check whether there is a playerstash object in the scene
		if (!PlayerFound) {
			GameObject playerStashsh = GameObject.Find ("PlayerStash(Clone)");

			if (playerStashsh != null) {
					PlayerFound = true;
				print("Player Connected!");
			}
		}

		if(PlayerFound){

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
				if(mousePos.x >= (right_screen_side - NavigationMarginLeftAndRight)){	Add_Right += 5.0f;	}
				if(mousePos.x <= 0 + NavigationMarginLeftAndRight){						Add_Right -= 5.0f;	}
				if(mousePos.y >= (top_screen_side - NavigationMarginTopAndBot)){		Add_Up += 5.0f;	}
				if(	mousePos.y <= 0 + NavigationMarginTopAndBot){						Add_Up -= 5.0f;	}

			}

			if (Input.GetAxis("Mouse ScrollWheel") > 0) {	Add_In += 2;	}
			if (Input.GetAxis("Mouse ScrollWheel") < 0) {	Add_In -= 2;	}


			GetComponent<Camera>().orthographicSize += Add_In;
			float sizeOfCamera = GetComponent<Camera>().orthographicSize;

			//////////////////////////////////////////////////////////////////////////////////////////
			////// KEYBOARD NAVIGATION /////////////////////////////////////////////////////////////// 
			//////////////////////////////////////////////////////////////////////////////////////////

			// APPLY DIRECTION
			if(Input.GetKey(KeyCode.UpArrow)){		Add_Up += 50.0f;		}
			if(Input.GetKey(KeyCode.DownArrow)){	Add_Down += 50.0f;		}
			if(Input.GetKey(KeyCode.LeftArrow)){	Add_Left += 50.0f; 		}
			if(Input.GetKey(KeyCode.RightArrow)){	Add_Right += 50.0f;		}

			// FINALIZE DIRECTION
			Vector3 direction = new Vector3(Add_Right-Add_Left, Add_Up-Add_Down, 0.0f );



			// SET CAMERA POSITION
			transform.position = Vector3.Lerp(transform.position, transform.position + (direction * sizeOfCamera/2), Time.deltaTime); 

		}
	}
}
