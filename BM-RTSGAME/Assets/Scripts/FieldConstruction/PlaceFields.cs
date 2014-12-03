using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlaceFields : MonoBehaviour {

	//====================================== Public
	public GameObject StdFields;
	public GameObject[] MoreStdFields;
	public GameObject CornerFields;
	public GameObject SideFields;
	public GameObject[] player;
	public Vector2 FieldDimensions;
	public Vector3 fieldPosition;

	//====================================== Hidden Public
	[HideInInspector]
	public int amountOfPlayers;
	public Vector2[] PlayerPositions;

	//====================================== Private
	private List<int> playerCoordinates;
	private float setXPos;
	private float setYPos;
	private float setZPos;
	private bool itsAPlayer;
	private bool isASide;
	private bool isACorner;
	private GameObject NetworkM;
	private GameObject aStar;
	private AstarPath aStarScript;
	
	void Awake(){
		// Asks for the amount of players. 
		NetworkM = GameObject.Find ("NetworkManager");
		amountOfPlayers = NetworkM.GetComponent<NetworkManager> ().NumberOfPlayers;
		PlayerPositions = new Vector2[amountOfPlayers];

		SetPlayerPositions ();
		GenerateMapFields ();
	}
		
	void Start () {

		aStar = GameObject.Find ("A*");
		aStarScript = aStar.GetComponent<AstarPath> ();
		aStarScript.Scan (); //Scan the level, for pathfinding. For obvious reasons, this needs to be done after the level has been generated.

	}

	void SetPlayerPositions(){
		//========================================================================= Player Positions
		// This section basically sets the player positions randomly, however, none
		// of them can be in the same row or coloumn. 
		//   NOT POSSIBLE      Possible
		//		OOO#OO 			O#OOOO
		// 		OOOOOO			OOOO#O
		//  	O#O#OO			OOO#OO
		
		playerCoordinates = new List<int>();
		
		//------------------ Random location but not outer field. 
		for (int AllPlayersGetCoordinates = 0; AllPlayersGetCoordinates < amountOfPlayers; AllPlayersGetCoordinates++){
			
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
			//--------------------------------------- Else check it all again.
			else{
				AllPlayersGetCoordinates--;
			}
			
			//--------------------------------------- Clear the checklist
			checklistX.Clear();
			checklistY.Clear();
		}
	}

	void GenerateMapFields(){
	
		//========================================================================= CALCULATE DIMENSIONS
		float sizeOfFieldX = StdFields.transform.lossyScale.x;
		float sizeOfFieldY = StdFields.transform.lossyScale.y;

		float setPosMiddleX = ((-StdFields.transform.lossyScale.x*(FieldDimensions[0]-1))/2);
		float setPosMiddleY = ((-StdFields.transform.lossyScale.y*(FieldDimensions[1]-1))/2);
	
		float setOriginX = fieldPosition[0];
		float setOriginY = fieldPosition[1];
		float setOriginZ = fieldPosition[2];

		//========================================================================= GENERATE MAP
		for (int i = 0; i < (int)FieldDimensions[0]; i += 1){
			for (int j = 0; j < (int)FieldDimensions[1]; j += 1){

				// Basically puts the fields where they need to be, depending on
				// both row/coloum number, its center and if it needs to be pushed. 
				// |Field|Field|Field|Field|Field|Field
				// |-----------|-----------| (sizeOfFieldX*i)
				// 					 |-----------| setPosMiddleX
				// 				    |-----------| setOriginX (normally not used)

				setXPos = (sizeOfFieldX*i) + setPosMiddleX + setOriginX;
				setYPos = (sizeOfFieldY*j) + setPosMiddleY + setOriginY;
				setZPos = setOriginZ;
				itsAPlayer = false;

				CheckForPlayerField(i,j);

				CheckForCornerField(i,j);

				CheckForSideField(i,j);

				CheckForStdField(i,j);

			}
		}
	}

	//========================================================================= CHECK IF PLAYER
	void CheckForPlayerField(int i, int j){
		//------------------------- CHECK IF PLAYER: : Instantiate player field
		int timesRunThrough = 0;
		int runThroughPlayers = 0;
		while (runThroughPlayers < (player.Length*2)){
			if(i == playerCoordinates[runThroughPlayers] && j == playerCoordinates[runThroughPlayers+1]){
				itsAPlayer = true; 

				(Instantiate (player[runThroughPlayers/2], new Vector3(setXPos, setYPos, setZPos), Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
				PlayerPositions[timesRunThrough] = new Vector2(setXPos, setYPos);
			}
			runThroughPlayers+=2;
			timesRunThrough++;
		}
	}

	//========================================================================= CHECK IF CORNER
	void CheckForCornerField(int i, int j){
		//------------------------- IF CORNER: Instantiate corner field and turn it
		isACorner = false;
		if (i == 0 && j == 0){
			(Instantiate (CornerFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
			isACorner = true;
		}
		
		if (i == ((int)FieldDimensions[0]-1) && j == 0){
			(Instantiate (CornerFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(90, Vector3.forward)) as GameObject).transform.parent = gameObject.transform;
			isACorner = true;
		}
		
		if (i == 0 && j == ((int)FieldDimensions[1]-1)){
			(Instantiate (CornerFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(270, Vector3.forward)) as GameObject).transform.parent = gameObject.transform;
			isACorner = true;
		}
		
		if (i == ((int)FieldDimensions[0]-1) && j == ((int)FieldDimensions[1]-1)){
			(Instantiate (CornerFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(180, Vector3.forward)) as GameObject).transform.parent = gameObject.transform;
			isACorner = true;
		}
	}

	//========================================================================= CHECK IF SIDE
	void CheckForSideField(int i, int j){
		//------------------------- IF A SIDE: 
		isASide = false;
		if (i == 0 && j != 0 && j != ((int)FieldDimensions[1]-1)){
			(Instantiate (SideFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
			isASide = true;
		}
		
		if (i == ((int)FieldDimensions[0]-1) && j != 0 && j != ((int)FieldDimensions[1]-1)){
			(Instantiate (SideFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(180, Vector3.forward)) as GameObject).transform.parent = gameObject.transform;
			isASide = true;
		}
		
		if (j == 0 && i != 0 && i != ((int)FieldDimensions[0]-1)){
			(Instantiate (SideFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(90, Vector3.forward)) as GameObject).transform.parent = gameObject.transform;
			isASide = true;
		}
		
		if (j == ((int)FieldDimensions[1]-1) && i != 0 && i != ((int)FieldDimensions[0]-1)){
			(Instantiate (SideFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.AngleAxis(270, Vector3.forward)) as GameObject).transform.parent = gameObject.transform;
			isASide = true;
		}
	}

	//========================================================================= CHECK IF STD FIELD
	void CheckForStdField(int i, int j){
		//------------------------- IF NO PLAYER AND CORNER: Instantiate standard field
		if (itsAPlayer == false && isACorner == false && isASide == false){
			
			int randomStdField = (int)Random.Range(0, MoreStdFields.Length+1);
			
			if (randomStdField==0){
				(Instantiate (StdFields, new Vector3(setXPos, setYPos, setZPos), Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
			}
			if (randomStdField>0){
				(Instantiate (MoreStdFields[randomStdField-1], new Vector3(setXPos, setYPos, setZPos), Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
			}
			
		}
	}



}
