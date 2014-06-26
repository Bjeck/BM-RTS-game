using UnityEngine;
using System.Collections;

public class PlaceFields : MonoBehaviour {
	
	public GameObject StdFields;
	public GameObject[] player;
	public Vector2 FieldDimensions;
	public Vector3 fieldPosition;



	// Use this for initialization
	void Start () {
	
		int[,] PlayerArray = new int[player.Length, 3];

		for (int RunTimes = 0; RunTimes < player.Length; RunTimes++){

			PlayerArray[RunTimes,0] = RunTimes;
			PlayerArray[RunTimes,1] = 1+(int)Random.Range(0, FieldDimensions[0]-2);
			PlayerArray[RunTimes,2] = 1+(int)Random.Range(0, FieldDimensions[1]-2);

			Debug.Log (PlayerArray[RunTimes,1]);
			Debug.Log (PlayerArray[RunTimes,2]);
		}

		for (int i = 0; i < (int)FieldDimensions[0]; i += 1){
			for (int j = 0; j < (int)FieldDimensions[1]; j += 1){

				float setXPosArr = (StdFields.transform.lossyScale.x*i);
				float setYPosArr = (StdFields.transform.lossyScale.y*j);
				float setPosMiddleX = ((-StdFields.transform.lossyScale.x*(FieldDimensions[0]-1))/2);
				float setPosMiddleY = ((-StdFields.transform.lossyScale.y*(FieldDimensions[1]-1))/2);
				float setOriginX = fieldPosition[0];
				float setOriginY = fieldPosition[1];
				float setOriginZ = fieldPosition[2];

				float setXPos = setXPosArr + setPosMiddleX + setOriginX;
				float setYPos = setYPosArr + setPosMiddleY + setOriginY;
				float setZPos = setOriginZ;

				for (int CheckIfPlayerPos = 0; CheckIfPlayerPos < player.Length; CheckIfPlayerPos++){

					if (i == PlayerArray[CheckIfPlayerPos,1] && j == PlayerArray[CheckIfPlayerPos,2]) {

					}

					else{
						Instantiate (StdFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.identity);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
