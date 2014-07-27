﻿using UnityEngine;
using System.Collections;

public class UserInterfaceBack : MonoBehaviour {

	public GameObject CameraSettings;

	private int bottomBarHeight = 30;
	private int TopBarHeight = 30;
	private float SliderWidth = 100.0f;
	private float CustomMargin = 20.0f;

	private Rect windowRect = new Rect (20, 20, 120, 50);
	
	void OnGUI(){

		GUI.backgroundColor = Color.yellow;
		GUI.Box (new Rect(0, 0, Screen.width, TopBarHeight), "ELECTRO");

		CameraSettings.GetComponent<CameraControl>().ZoomStartPosition = GUI.HorizontalScrollbar (new Rect (Screen.width-(SliderWidth+CustomMargin), TopBarHeight/2, SliderWidth, 0), CameraSettings.GetComponent<CameraControl>().ZoomStartPosition, 1.0f, 1.0f, 5.0f);
	
	}


}