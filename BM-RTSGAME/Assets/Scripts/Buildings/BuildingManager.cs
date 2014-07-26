using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BuildingManager : MonoBehaviour {

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

	///////////////////////// OTHER ////////////////////////
	GameObject instance;
	public GameObject building;
	GameObject Astar;
	SpriteRenderer sprtR;
	string name = null;
	AstarPath astarpath;

	// Use this for initialization
	void Start () {
		Astar = GameObject.Find ("A*");
		astarpath = Astar.GetComponent<AstarPath> ();

		GameObject mouse = GameObject.Find ("Main Camera");
		Mouse mouseS = mouse.GetComponent<Mouse> ();
		player1 = mouseS.player1;


	}
	
	// Update is called once per frame
	void Update () {

		if (PopulationPlayer1 != PopulationPlayer1Tmp){
			print (PopulationPlayer1);
			PopulationPlayer1Tmp = PopulationPlayer1;
		}

		if(resources.Count == 4){
			//Debug.Log ("Resource registered!");
		}

		if (isDragging) { //dragging the placeable object with the mouse
			//Debug.Log("DRAGS!");
			Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mPos.z = -1;
			mPos.x = Mathf.Ceil(mPos.x);
			mPos.y = Mathf.Ceil(mPos.y);
			instance.transform.position = mPos;
		}

		if (Input.GetMouseButtonUp (0) && isDragging) {
				PlaceBuilding ();
		}

		if (Input.GetKeyDown (KeyCode.Escape) && isDragging) {
			CancelPlacement();		
		}

		if(Input.anyKeyDown && !isDragging){

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
			else{
				name = null;
			}
			
			if(name != null){
				isDragging = true;
				instance = (GameObject)Instantiate(Resources.Load(name,typeof(GameObject)));
				sprtR = instance.GetComponent<SpriteRenderer>();
				sprtR.color = Color.gray;
				Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mPos.z = -1;
				instance.transform.position = mPos;
			}
		}

		if (!IsLegalPosition () && isDragging) {
			//Debug.Log("THIS ONE!");
			sprtR = instance.GetComponent<SpriteRenderer> ();
			sprtR.color = Color.red;
		} 	else if(isDragging){
			sprtR = instance.GetComponent<SpriteRenderer>();
			sprtR.color = Color.gray;
		}
	}

	void PlaceBuilding(){
	//CHECKING IF BUILDING CAN BE PLACED

		//print (resources.Count);
		if (resources.Count == 4 && name == "resource_1"){
			//Debug.Log ("Passed!");
		}
		if (!IsLegalPosition()) {
			//Debug.Log ("Cant place there!");
			return;		
		}

			//ACTUALLY PLACING BUILDING
			building = (GameObject)Instantiate(Resources.Load(name,typeof(GameObject)));
			//Debug.Log (building.transform.localScale.x / 2);
			building.layer = 10;
			//Debug.Log("PLACE OBJECT!"+building.name);
			sprtR = building.GetComponent<SpriteRenderer>();
			sprtR.color = Color.white;
			Destroy (instance);
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
				}
		StartCoroutine (scanLevel ());
		resources.Clear();
		//print (resources.Count);
	}
	
	IEnumerator scanLevel(){
		float t = 0;
		while(t<0.01f){
			t+=Time.deltaTime;
			yield return 0;
		}
		astarpath.Scan();
		yield return 0;
	}


	bool IsLegalPosition(){
		if (colliders.Count > 0) {
			//Debug.Log ("ELSE IF 1");
			return false;
		}else if(resources.Count > 0 && name != "resource_1"){
			//Debug.Log ("ELSE IF 2");
			return false;
		}
		if(name == "resource_1" && resources.Count < 4){
			//Debug.Log ("ELSE IF 3");
			return false;
		}

		return true;
	}

	void CancelPlacement(){
		isDragging = false;
		Destroy (instance);
	}


}
