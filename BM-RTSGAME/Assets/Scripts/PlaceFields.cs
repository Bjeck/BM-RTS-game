using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlaceFields : MonoBehaviour {
	
	public GameObject StdFields;
	public GameObject[] player;
	public Vector2 FieldDimensions;
	public Vector3 fieldPosition;

	// Use this for initialization
	void Start () {
	
		List<int> playerCoordinates = new List<int>();

		//------------------ Random location but not outer field. 
		for (int AllPlayersGetCoordinates = 0; AllPlayersGetCoordinates < player.Length; AllPlayersGetCoordinates++){



			//--------------------------------------- Create lists
			List<int> checklistX = new List<int>();
			List<int> checklistY = new List<int>();

			//--------------------------------------- Put Number into checklist
			checklistX.Add(1+(int)Random.Range(0, FieldDimensions[0]-2));
			checklistY.Add(1+(int)Random.Range(0, FieldDimensions[1]-2));

			//--------------------------------------- Are numbers in the playerCoordinates?
			bool XisClear = playerCoordinates.Any(item => checklistX.Contains(item));
			bool YisClear = playerCoordinates.Any(item => checklistY.Contains(item));


			//--------------------------------------- If they are, add it to the playerCoordinates list
			if (!XisClear && !YisClear){
				Debug.Log (checklistX[0]);
				Debug.Log (checklistY[0]);
				playerCoordinates.Add(checklistX[0]);
				playerCoordinates.Add(checklistY[0]);
			}
			else{
				AllPlayersGetCoordinates--;
			}

			//--------------------------------------- Clear the checklist
			checklistX.Clear();
			checklistY.Clear();
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

				//------------------------- CHECK IF PLAYER
				bool itsAPlayer = false;
				int runTimes = 0;
				int runThroughPlayers = 0;
				while (runTimes < player.Length){
					if(i == playerCoordinates[runThroughPlayers] && j == playerCoordinates[runThroughPlayers+1]){
						itsAPlayer = true; 
						Instantiate (player[runTimes], new Vector3(setXPos, setYPos, setZPos), Quaternion.identity);
					}

					runThroughPlayers+=2;
					runTimes++;
				}

				//------------------------- IF NO PLAYER IS PLACED
				if (itsAPlayer == false){
					Instantiate (StdFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.identity);	
				}

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
