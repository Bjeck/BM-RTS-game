using UnityEngine;
using System.Collections;

public class PlaceFields : MonoBehaviour {
	
	public GameObject StdFields;
	public GameObject Player1;
	public GameObject Player2;
	public Vector2 FieldDimensions;
	public Vector3 fieldPosition;

	private int toI_Player1;
	private int toJ_Player1;
	private int toI_Player2;
	private int toJ_Player2;
	
	// Use this for initialization
	void Start () {

		Debug.Log(FieldDimensions[1]);

		for (int i = 0; i <= (int)FieldDimensions[0]; i += 1){

			for (int j = 0; j <= (int)FieldDimensions[1]; j += 1){

				float setXPosArr = (StdFields.transform.lossyScale.x*i);
				float setYPosArr = (StdFields.transform.lossyScale.y*j);
				float setPosMiddleX = ((-StdFields.transform.lossyScale.x*FieldDimensions[0])/2);
				float setPosMiddleY = ((-StdFields.transform.lossyScale.y*FieldDimensions[1])/2);
				float setOriginX = fieldPosition[0];
				float setOriginY = fieldPosition[1];
				float setOriginZ = fieldPosition[2];

				float setXPos = setXPosArr + setPosMiddleX + setOriginX;
				float setYPos = setYPosArr + setPosMiddleY + setOriginY;
				float setZPos = setOriginZ;

				Instantiate (StdFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.identity);


			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
