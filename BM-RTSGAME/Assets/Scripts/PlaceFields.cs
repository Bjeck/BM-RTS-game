using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlaceFields : MonoBehaviour {

	public GameObject StdFields;
	public GameObject[] MoreStdFields;
	public GameObject CornerFields;
	public GameObject SideFields;
	public GameObject[] player;
	public Vector2 FieldDimensions;
	public Vector3 fieldPosition;

	GameObject aStar;
	AstarPath aStarScript;

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

				//------------------------- CHECK IF PLAYER: : Instantiate player field
				bool itsAPlayer = false;
				int runTimes = 0;
				int runThroughPlayers = 0;
				while (runTimes < player.Length){
					if(i == playerCoordinates[runThroughPlayers] && j == playerCoordinates[runThroughPlayers+1]){
						itsAPlayer = true; 
						Instantiate (player[runTimes], new Vector3(setXPos, setYPos, setZPos), Quaternion.identity);

						//if(runTimes==0){														//THIS IS for setting the starting position of the camera at the player's starting point. Disabled for now.
						//	Camera.main.transform.position = new Vector3(setXPos,setYPos, -10);
						//}
					}

					runThroughPlayers+=2;
					runTimes++;
				}
			
				//------------------------- IF CORNER: Instantiate corner field and turn it
				bool isACorner = false;
				if (i == 0 && j == 0){
					Instantiate (CornerFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.identity);
					isACorner = true;
				}

				if (i == ((int)FieldDimensions[0]-1) && j == 0){
					Instantiate (CornerFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(90, Vector3.forward));
					isACorner = true;
				}

				if (i == 0 && j == ((int)FieldDimensions[1]-1)){
					Instantiate (CornerFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(270, Vector3.forward));
					isACorner = true;
				}

				if (i == ((int)FieldDimensions[0]-1) && j == ((int)FieldDimensions[1]-1)){
					Instantiate (CornerFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(180, Vector3.forward));
					isACorner = true;
				}

				//------------------------- IF A SIDE: 
				bool isASide = false;
				if (i == 0 && j != 0 && j != ((int)FieldDimensions[1]-1)){
					Instantiate (SideFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.identity);
					isASide = true;
				}

				if (i == ((int)FieldDimensions[0]-1) && j != 0 && j != ((int)FieldDimensions[1]-1)){
					Instantiate (SideFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(180, Vector3.forward));
					isASide = true;
				}

				if (j == 0 && i != 0 && i != ((int)FieldDimensions[0]-1)){
					Instantiate (SideFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(90, Vector3.forward));
					isASide = true;
				}

				if (j == ((int)FieldDimensions[1]-1) && i != 0 && i != ((int)FieldDimensions[0]-1)){
					Instantiate (SideFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(270, Vector3.forward));
					isASide = true;
				}

				//------------------------- IF NO PLAYER AND CORNER: Instantiate standard field
				if (itsAPlayer == false && isACorner == false && isASide == false){

					int randomStdField = (int)Random.Range(0, MoreStdFields.Length+1);

					if (randomStdField==0){
						Instantiate (StdFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.identity);
					}
					if (randomStdField>0){
						Instantiate (MoreStdFields[randomStdField-1], new Vector3(setXPos, setYPos, setZPos), Quaternion.identity);
					}
				
				}
			}
		}

		aStar = GameObject.Find ("A*");
		aStarScript = aStar.GetComponent<AstarPath> ();
		aStarScript.Scan (); //Scan the level, for pathfinding. For obvious reasons, this needs to be done after the level has been generated.

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
