using UnityEngine;
using System.Collections;

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

		if (isTargeting) {

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

	public override bool Do(){
		if (!base.Do ()) {
			return false;		
		}

		//Debug.Log ("DIRECTTARGET DO");
		targetCursor = (GameObject)Instantiate(Resources.Load("Effects/targetCursor",typeof(GameObject)));
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




	public void Cast(){
		caster.GetComponent<Unit>().TakeMemory (cost);
		StopTargeting ();
		aMan.listOfAbilities.Clear ();
	}



	public void StopTargeting(){
		target = null;
		isTargeting = false;
		Destroy (targetCursor);
		aMan.listOfAbilities.Clear ();
	}

}
