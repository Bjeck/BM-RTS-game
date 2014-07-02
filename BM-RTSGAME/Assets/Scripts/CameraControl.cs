﻿using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	//-------------------------------------------------------- Public
	public int marginForPan = 70;
	public int marginForTilt = 70;
	public int panSensitivity = 10;
	public int tiltSensitivity = 10;
	public float AccelerationSpeed = 3.0f;
	public float ZoomStartPosition = 5.0f;
	public float ZoomSpeed = 0.2f;
	public float MinZoomDistance = 1.0f;
	public float MaxZoomDistance = 5.0f;
	
	//-------------------------------------------------------- Private
	private int right_screen_side;
	private int left_screen_side;
	private int top_screen_side;
	private int bottom_screen_side;
	private float accR = 1.0f, accL = 1.0f, accT = 1.0f, accB = 1.0f;
	private Vector3 mousePos = new Vector3(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
	
		//-------------------------------------------------------- Border Control 
		right_screen_side = Screen.width;
		left_screen_side = 0;
		
		top_screen_side = Screen.height;
		bottom_screen_side = 0;

	}
	
	// Update is called once per frame
	void Update () {
	
		
		//-------------------------------------------------------- Mouse Position
		mousePos = Input.mousePosition;
		camera.orthographicSize = ZoomStartPosition;
		
//		//-------------------------------------------------------- Acceleration in margins
//		accR = AccelerationSpeed 	* 	1.0f/marginForPan*(mousePos.x-(Screen.width-marginForPan));
//		accL = AccelerationSpeed 	* 	1.0f/marginForPan*(-mousePos.x+marginForPan);
//		
//		accT = AccelerationSpeed 	* 	1.0f/marginForTilt*(mousePos.y-(Screen.height-marginForTilt));
//		accB = AccelerationSpeed 	* 	1.0f/marginForTilt*(-mousePos.y+marginForTilt);
//		
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && ZoomStartPosition > MinZoomDistance) // forward
		{
			ZoomStartPosition -= ZoomSpeed;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0 && ZoomStartPosition < MaxZoomDistance) // back
		{
			ZoomStartPosition += ZoomSpeed;
		}

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
			transform.position = Vector3.Lerp(transform.position, transform.position + transform.up/ZoomStartPosition * (tiltSensitivity * accT), Time.deltaTime); 
		}
		
		//-------------------------------------------------------- GO DOWN
		if (mousePos.y < (bottom_screen_side+marginForTilt)){	
			transform.position = Vector3.Lerp(transform.position, transform.position - transform.up/ZoomStartPosition * (tiltSensitivity * accB), Time.deltaTime); 
		}


	}
}
