using UnityEngine;
using System.Collections;

/// <summary>
/// Direct target is for abilities that need a direct target. this is abilities like zap, swap, etc.
/// </summary>

public class DirectTarget : Ability {

	public GameObject target;
	public float range;
	public bool isTargeting = false;
	public float dist = 0;


	// Use this for initialization
	public void Start () {
		base.Start ();
	
	}
	
	// Update is called once per frame
	public void Update () {
		base.Update ();
		aMan.isTargetingAbility = isTargeting;

		if (isTargeting) { //updates the target cursor to mouse position and color to see range.

			Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			temp.z = -1f;
			targetCursor.transform.position = temp;

			dist = Vector3.Distance(targetCursor.transform.position,caster.transform.position); 
			//Debug.Log(dist);
			if(dist > range){
				//Debug.Log("OUT OF RANGE");
				targetCursor.GetComponent<SpriteRenderer> ().sprite = targetCursorRed;
			}
			else {
				//Debug.Log("IN RANGE: "+targetCursorGreen);
				targetCursor.GetComponent<SpriteRenderer> ().sprite = targetCursorGreen;
			}

			if(Input.GetKeyDown(KeyCode.Escape)){
				StopTargeting();
			}
		}
	}

	//Activate targeting of the ability. THIS DOESN'T ACTUALLY CAST THE ABILITY.
	public override bool Do(){
		if (!base.Do ()) { //base.Do checks if unit has enough memory to cast. if not, return.
			return false;		
		}

		//Debug.Log ("DIRECTTARGET DO");
		targetCursor = (GameObject)Instantiate(Resources.Load("Effects/targetCursor",typeof(GameObject))); //spawns target cursor and have it follow mouse.
		targetCursor.GetComponent<SpriteRenderer> ().sprite = targetCursorGreen;
		Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		temp.z = -1f;
		
		targetCursor.transform.position = temp;
		//isTargeting = true;

		if (isTargeting) {
			return false;		
		}
		isTargeting = true;
		return true;
	}




	public void Cast(){ // Cast the ability.
		caster.GetComponent<Unit>().TakeMemory (cost);
		StopTargeting ();
	}



	public void StopTargeting(){
		target = null;
		isTargeting = false;
		Destroy (targetCursor);
		aMan.listOfCurrentlyCastableAbilities.Clear ();
	}

}
