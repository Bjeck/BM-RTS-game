using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// SCRIPT THAT MANAGES ALL BUILDINGS
/// </summary>

public class BuildingManager : MonoBehaviour {
	
	private static BuildingManager _instance;
	
	public static BuildingManager instance{
		get{
			if (_instance == null){
				_instance = GameObject.FindObjectOfType<BuildingManager>();
			}
			return _instance;
		}
	}

	///////////////////////// PUBLIC ////////////////////////
	public bool isDragging = false;
	public List<Collider> colliders = new List<Collider>();
	public List<Collider> resources = new List<Collider>();
	public bool player1 = false;
	public int PopulationPlayer1 = 0;
	public int PopulationLimit = 10;

	///////////////////////// PRIVATE ///////////////////////
	private bool ResourceDetected = false;

	private int PopulationPlayer1Tmp;

	private List<Building_UnitProduction> currentlySelectedUnitBuildings = new List<Building_UnitProduction>();
	private List<Building_Upgrade> currentlySelectedUpgradeBuildings = new List<Building_Upgrade>();

	///////////////////////// OTHER ////////////////////////
	GameObject buildingInstance;
	public GameObject building;
	GameObject Astar;
	SpriteRenderer sprtR;
	string name = null;
	AstarPath astarpath;
	Mouse mouseS;

	UnitManager unitMana;

	// Use this for initialization
	void Start () {
		Astar = GameObject.Find ("A*");

		if(Astar != null)
			astarpath = Astar.GetComponent<AstarPath> ();

		GameObject mouse = GameObject.Find ("Main Camera");
		mouseS = mouse.GetComponent<Mouse> ();
		player1 = mouseS.player1;
		unitMana = gameObject.GetComponent<UnitManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PopulationPlayer1 != PopulationPlayer1Tmp){
//			print (PopulationPlayer1);
			PopulationPlayer1Tmp = PopulationPlayer1;
		}

		if(resources.Count == 4){
			//Debug.Log ("Resource registered!");
		}


//------------------------ DRAGGING AND PLACEMENT

		if (isDragging) { //dragging the placeable object with the mouse
			//Debug.Log("DRAGS!");
			Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mPos.z = -1;
			mPos.x = Mathf.Ceil(mPos.x);
			mPos.y = Mathf.Ceil(mPos.y);
			buildingInstance.transform.position = mPos;
		}

		if (Input.GetMouseButtonUp (0) && isDragging) {
				PlaceBuilding ();
		}

		if (Input.GetKeyDown (KeyCode.Escape) && isDragging) {
			CancelPlacement();		
		}

		if(Input.anyKeyDown && !isDragging){
			if (transform.parent.networkView.isMine){
				//Debug.Log("KEY GOT!");
				if(Input.GetKey(KeyCode.W) && PopulationPlayer1<PopulationLimit){		//HERE IS WHERE WE ADD MORE BUILDINGS!
					name = "building_1";
				}
				else if(Input.GetKey(KeyCode.T) && PopulationPlayer1<PopulationLimit){
					name = "building_2";
				}
				else if(Input.GetKey(KeyCode.R)){
					name = "resource_1";
				}
				else if(Input.GetKey(KeyCode.D)){
					name = "building_a_1";
				}
				else if(Input.GetKey (KeyCode.E)){
					name = "building_u_1";
				}
				else{
					name = null;
				}
			}
			
			SpecifyBuildingToPlace(name);
		}

		if (!IsLegalPosition () && isDragging) {
			//Debug.Log("THIS ONE!");
			sprtR = buildingInstance.GetComponent<SpriteRenderer> ();
			sprtR.color = Color.red;
		} 	else if(isDragging){
			sprtR = buildingInstance.GetComponent<SpriteRenderer>();
			sprtR.color = Color.gray;
		}

	}

	//Begins to place building (Instantiates the hover version of the building)
	public void SpecifyBuildingToPlace(string namef){
		if(namef != null){
			isDragging = true;
			buildingInstance = (GameObject)Instantiate(Resources.Load(namef,typeof(GameObject)));
			sprtR = buildingInstance.GetComponent<SpriteRenderer>();
			sprtR.color = Color.gray;
			Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mPos.z = -1;
			buildingInstance.transform.position = mPos;
		}
	}

	//Placing the building on the ground.
	void PlaceBuilding(){
	//CHECKING IF BUILDING CAN BE PLACED
		//print (resources.Count);
		if (resources.Count == 4 && name == "resource_1"){
			//Debug.Log ("Passed!");
		}
		if (!IsLegalPosition()) {
			Debug.Log ("Cant place there!");
			return;		
		}

			//ACTUALLY PLACING BUILDING
		building = (GameObject)Network.Instantiate(Resources.Load(name,typeof(GameObject)), transform.position, Quaternion.identity,0);
			//Debug.Log (building.transform.localScale.x / 2);
			building.layer = 10;
			//Debug.Log("PLACE OBJECT!"+building.name);
			sprtR = building.GetComponent<SpriteRenderer>();
			sprtR.color = Color.white;
			Destroy (buildingInstance);
			RaycastHit hit;

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100f)){
				Vector3 temp = hit.point;
				temp.z = -1;
				temp.x = Mathf.Ceil(temp.x);
				temp.y = Mathf.Ceil(temp.y);
				
				building.transform.position = temp;
				building.GetComponent<Building>().isPlaced = true;
				building.GetComponent<Building>().player1 = player1;
				//Debug.Log ("PLACED!");
				isDragging = false;
				name = null;

		StartCoroutine (scanLevel ());
		resources.Clear();
		//print (resources.Count);
			}
	}


	//Separate function to check legal position. For resources.
	bool IsLegalPosition(){

		LayerMask layerMask = (1 << 8);
		RaycastHit hit;

		if (colliders.Count > 0) {
			//Debug.Log ("ELSE IF 1");
			return false;
		}else if(resources.Count > 0 && name != "resource_1"){
			//Debug.Log ("ELSE IF 2");
			return false;
		}
		else if(!Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f, layerMask)) {
			return false;
		}
		if(name == "resource_1" && resources.Count < 4){
			//Debug.Log ("ELSE IF 3");
			return false;
		}

		return true;
	}

	//Cancels placement of the current building.
	void CancelPlacement(){
		isDragging = false;
		Destroy (buildingInstance);
	}


	
//SCANNNING LEVEL
	
	public void scanTheLevel(){
		StartCoroutine (scanLevel ());
	}
	
	IEnumerator scanLevel(){ //Scans the level. This is called every time a new building is placed, so it get's added as an obstacle for units.
	//	Debug.Log ("SCAN!");
		float t = 0;
		while(t<0.01f){
			//Debug.Log ("SCAN!!");
			t+=Time.deltaTime;
			yield return 0;
		}
		//Debug.Log ("SCAN!!!");
		if(Astar == null){
		//	Debug.Log ("SCAN!! !!");
			Astar = GameObject.Find ("A*");
			astarpath = Astar.GetComponent<AstarPath> ();
		}
		//Debug.Log ("SCAN!! !!!");
		astarpath.Scan();
		yield return 0;
	}





// ---------------------------- UNIT PRODUCTION MANAGEMENT

	public void ExecuteOrder(float time, string name){ //Right now, only used for building units in unit production buildings. Maybe more later??
		if (mouseS.buildingsSelected.Count > 0) { //first, how many buildings are selected?
			foreach(Building b in mouseS.buildingsSelected){
				if(b.isUnitBuilding == true){
					currentlySelectedUnitBuildings.Add(b.gameObject.GetComponent<Building_UnitProduction>());
					//Debug.Log(currentlySelectedUnitBuildings.Count);
				}
			}
		}

		if (currentlySelectedUnitBuildings.Count == 0) { //if there, for some reason, aren't any unit buildings selected, abort.
			Debug.Log("This shouldn't happen");
			return;		
		}
		else if(currentlySelectedUnitBuildings.Count == 1){ //only one building, easy. Build unit if possible.
//			Debug.Log("CHECKIN FOR AVAILABILITY TO CONSTRUCT UNIT: "+currentlySelectedUnitBuildings[0].isConstructing);
			if(!currentlySelectedUnitBuildings[0].isConstructing)
				StartCoroutine(currentlySelectedUnitBuildings[0].ConstructUnit(time, name));
		}
		else if(currentlySelectedUnitBuildings.Count > 1){
		//	Debug.Log("MORE THAN ONE: "+currentlySelectedUnitBuildings.Count);
			int i = 0;
			foreach(Building_UnitProduction bu in currentlySelectedUnitBuildings){ //if more buildings, go through each and check if they are free to build. If so, build and stop checking.
//				Debug.Log("CHECKING "+bu+" FOR AVAILABILITY TO CONSTRUCT UNIT: "+currentlySelectedUnitBuildings[i].isConstructing);
				if(!currentlySelectedUnitBuildings[i].isConstructing){
					StartCoroutine(currentlySelectedUnitBuildings[i].ConstructUnit(time, name));
					currentlySelectedUnitBuildings.Clear ();
					return;
				}
				i++;
			}
		}
		currentlySelectedUnitBuildings.Clear ();
	}




//------------------------- UPGRADING MANAGEMENT

	public void UpgradeSomething(float time, string name){ //This is the same as the ExecuteOrder, but for upgrades instead.
		if (mouseS.buildingsSelected.Count > 0) { //first, how many buildings are selected?
			foreach(Building b in mouseS.buildingsSelected){
				//Debug.Log("Is this an upgrade building?"+b.isUpgradeBuilding);
				if(b.isUpgradeBuilding == true){ //if it is an upgrade building, add it to the current list.
					currentlySelectedUpgradeBuildings.Add(b.gameObject.GetComponent<Building_Upgrade>());
					//Debug.Log(currentlySelectedUpgradeBuildings.Count);
				}
			}
		}

		if (currentlySelectedUpgradeBuildings.Count == 0) { //error.
			Debug.Log("This shouldn't happen");
			return;		
		}
		else if(currentlySelectedUpgradeBuildings.Count == 1){ //if one is selected, set this to upgrade (if it isn't upgrading already).
			if(!currentlySelectedUpgradeBuildings[0].isConstructing){
				StartCoroutine(currentlySelectedUpgradeBuildings[0].ConstructUpgrade(time, name));
			}
		}
		else if(currentlySelectedUpgradeBuildings.Count > 1){ //If more than one. Find the first one that isn't doing anything, and set that to start upgrading.
			int i = 0;
			foreach(Building_Upgrade bu in currentlySelectedUpgradeBuildings){
				if(!currentlySelectedUpgradeBuildings[i].isConstructing){
					StartCoroutine(currentlySelectedUpgradeBuildings[i].ConstructUpgrade(time, name));
					currentlySelectedUpgradeBuildings.Clear ();
					return;
				}
				i++;
			}
		}
		currentlySelectedUpgradeBuildings.Clear ();
	}





}
