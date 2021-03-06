﻿using UnityEngine;
using System.Collections;

public class PlaceFields : MonoBehaviour {

	public GameObject Grass;
	public GameObject Player1;
	public GameObject Player2;
	public int GameFields;
	private int toI_Player1;
	private int toJ_Player1;
	private int toI_Player2;
	private int toJ_Player2;
	
	// Use this for initialization
	void Start () {

		int Scale = (int)Grass.transform.localScale.x;

		//---------------------------------------- GENERATE CORNER POSITIONS
		int TBLR = (int)Random.Range(-10, 10);

		if(TBLR <= -5)	{	
			toI_Player1 = (int)Random.Range(-(GameFields/Scale),0)*10; 		toI_Player2 = (int)Random.Range(0,(GameFields/Scale))*10;
			toJ_Player1 = (int)Random.Range(-(GameFields/Scale),0)*10;		toJ_Player2 = (int)Random.Range(0,(GameFields/Scale))*10;
		};

		if(TBLR <= 0 && TBLR > -5)	{	
			toI_Player1 = (int)Random.Range(-(GameFields/Scale),0)*10; 		toI_Player2 = (int)Random.Range(0,(GameFields/Scale))*10;
			toJ_Player1 = (int)Random.Range((GameFields/Scale),0)*10;		toJ_Player2 = (int)Random.Range(0,-(GameFields/Scale))*10;
		};

		if(TBLR <= 5 && TBLR > 0)	{	
			toI_Player1 = (int)Random.Range((GameFields/Scale),0)*10; 		toI_Player2 = (int)Random.Range(0,-(GameFields/Scale))*10;
			toJ_Player1 = (int)Random.Range(-(GameFields/Scale),0)*10;		toJ_Player2 = (int)Random.Range(0,(GameFields/Scale))*10;
		};

		if(TBLR <= 10 && TBLR > 5)	{	
			toI_Player1 = (int)Random.Range((GameFields/Scale),0)*10; 		toI_Player2 = (int)Random.Range(0,-(GameFields/Scale))*10;
			toJ_Player1 = (int)Random.Range((GameFields/Scale),0)*10;		toJ_Player2 = (int)Random.Range(0,-(GameFields/Scale))*10;
		};

		Debug.Log(toI_Player1);
		Debug.Log(toJ_Player1);

		for(int i = -GameFields; i <= GameFields; i += Scale){
			for(int j = -GameFields; j <= GameFields; j += Scale){
				if (i == toI_Player1 && j == toJ_Player1){
					Instantiate(Player1, new Vector3(i, j, 100), Quaternion.identity);
				}
				else if (i == toI_Player2 && j == toJ_Player2){
					Instantiate(Player2, new Vector3(i, j, 100), Quaternion.identity);
				}
				else{
					Instantiate(Grass, new Vector3(i, j, 100), Quaternion.identity);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
