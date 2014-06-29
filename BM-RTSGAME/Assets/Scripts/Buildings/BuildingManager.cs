using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour {

	public bool isDragging = false;
	GameObject instance;
	GameObject building;
	SpriteRenderer sprtR;
	string name = null;
	public List<Collider> colliders = new List<Collider>();


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (isDragging) { //dragging the placeable object with the mouse
			//Debug.Log("DRAGS!");
			Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mPos.z = -1;
			mPos.x = Mathf.Ceil(mPos.x);
			mPos.y = Mathf.Ceil(mPos.y);
			instance.transform.position = mPos;

		}

		if (Input.GetMouseButtonUp (0) && isDragging) 
			PlaceBuilding ();


		if(Input.anyKeyDown && !isDragging){

			//Debug.Log("KEY GOT!");
			if(Input.GetKey(KeyCode.W)){		//HERE IS WHERE WE ADD MORE BUILDINGS!
				name = "building_1";
			}
			else if(Input.GetKey(KeyCode.T)){
				name = "building_2";
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
			sprtR = instance.GetComponent<SpriteRenderer> ();
			sprtR.color = Color.red;
		} else if(isDragging){
			sprtR = instance.GetComponent<SpriteRenderer>();
			sprtR.color = Color.gray;
		}
	}
	


	void PlaceBuilding(){
	//CHECKING IF BUILDING CAN BE PLACED

		if (!IsLegalPosition ()) {
			return;		
		}

	//ACTUALLY PLACING BUILDING
		building = (GameObject)Instantiate(Resources.Load(name,typeof(GameObject)));
		//Debug.Log (building.transform.localScale.x / 2);
		building.layer = 10;
		Debug.Log("PLACE OBJECT!"+building.name);
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
			isDragging = false;
			name = null;
		}
	}

	IEnumerator Cantplace(){
		sprtR = instance.GetComponent<SpriteRenderer>();
		sprtR.color = Color.red;
		float t = 0;
		while(t<1){
			t+=Time.deltaTime;
			yield return 0;
		}
		sprtR.color = Color.white;
		yield return 0;
	}



	bool IsLegalPosition(){
		if (colliders.Count > 0) {
			//StartCoroutine(Cantplace());
			return false;
		}
		return true;
	}




	
}
