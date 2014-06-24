using UnityEngine;
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

		int TBLR = (int)Random.Range(-10, 10);
		if(TBLR <= -5)				{	toI_Player1 = -GameFields; 	toJ_Player1 = -GameFields;	toI_Player2 = GameFields; 	toJ_Player2 = GameFields; 	};
		if(TBLR <= 0 && TBLR > -5)	{	toI_Player1 = -GameFields; 	toJ_Player1 = GameFields;	toI_Player2 = GameFields; 	toJ_Player2 = -GameFields;	};
		if(TBLR <= 5 && TBLR > 0)	{	toI_Player1 = GameFields; 	toJ_Player1 = -GameFields;	toI_Player2 = -GameFields; 	toJ_Player2 = GameFields;	};
		if(TBLR <= 10 && TBLR > 5)	{	toI_Player1 = GameFields; 	toJ_Player1 = GameFields;	toI_Player2 = -GameFields; 	toJ_Player2 = -GameFields;	};
		Debug.Log(TBLR);

		int Scale = (int)Grass.transform.localScale.x;

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
