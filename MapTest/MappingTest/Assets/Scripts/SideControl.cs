using UnityEngine;
using System.Collections;

//------------------------ PUT ON MAIN CAMERA

public class SideControl : MonoBehaviour {

	//-------------------------------------------------------- Public
	public int marginForPan = 70;
	public int marginForTilt = 70;
	public int panSensitivity = 10;
	public int tiltSensitivity = 10;
	public float AccelerationSpeed = 3.0f;

	//-------------------------------------------------------- Private
	private int right_screen_side;
	private int left_screen_side;
	private int top_screen_side;
	private int bottom_screen_side;
	private float accR = 0.0f, accL = 0.0f, accT = 0.0f, accB = 0.0f;
	private Vector3 mousePos = new Vector3(0.0f, 0.0f, 0.0f);
	
	void Start () {

		//-------------------------------------------------------- Border Control 
		right_screen_side = Screen.width;
		left_screen_side = 0;

		top_screen_side = Screen.height;
		bottom_screen_side = 0;

	}

	void Update () {


		//-------------------------------------------------------- Mouse Position
		mousePos = Input.mousePosition;

		//-------------------------------------------------------- Acceleration in margins
		accR = AccelerationSpeed 	* 	1.0f/marginForPan*(mousePos.x-(Screen.width-marginForPan));
		accL = AccelerationSpeed 	* 	1.0f/marginForPan*(-mousePos.x+marginForPan);

		accT = AccelerationSpeed 	* 	1.0f/marginForTilt*(mousePos.y-(Screen.height-marginForTilt));
		accB = AccelerationSpeed 	* 	1.0f/marginForTilt*(-mousePos.y+marginForTilt);


		//-------------------------------------------------------- GO RIGHT
		if (mousePos.x > (right_screen_side-marginForPan)){ 	
			transform.position = Vector3.Lerp(transform.position, transform.position + transform.right * (tiltSensitivity * accR), Time.deltaTime); 
		}

		//-------------------------------------------------------- GO LEFT
		if (mousePos.x < (left_screen_side+marginForPan)){		
			transform.position = Vector3.Lerp(transform.position, transform.position - transform.right * (tiltSensitivity * accL), Time.deltaTime); 
		}

		//-------------------------------------------------------- GO UP
		if (mousePos.y > (top_screen_side-marginForTilt)){ 		
			transform.position = Vector3.Lerp(transform.position, transform.position + transform.up * (tiltSensitivity * accT), Time.deltaTime); 
		}

		//-------------------------------------------------------- GO DOWN
		if (mousePos.y < (bottom_screen_side+marginForTilt)){	
			transform.position = Vector3.Lerp(transform.position, transform.position - transform.up * (tiltSensitivity * accB), Time.deltaTime); 
		}




	}
}
