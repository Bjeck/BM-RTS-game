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

		if (isTargeting) {
			Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			temp.z = -1f;
			targetCursor.transform.position = temp;

			dist = Vector3.Distance(targetCursor.transform.position,caster.transform.position);
			if(dist > range){
				targetCursor.GetComponent<SpriteRenderer> ().sprite = targetCursorRed;
			}
			else {
				targetCursor.GetComponent<SpriteRenderer> ().sprite = targetCursorGreen;
			}

			if(Input.GetKeyDown(KeyCode.Escape)){
				StopTargeting();
			}
		}
	}

	public override void Do(){
		base.Do ();
		Debug.Log ("DIRECTTARGET DO");
		targetCursor = (GameObject)Instantiate(Resources.Load("Effects/targetCursor",typeof(GameObject)));
		targetCursor.GetComponent<SpriteRenderer> ().sprite = targetCursorGreen;
		Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		temp.z = -1f;
		
		targetCursor.transform.position = temp;
		isTargeting = true;
	}


	public void StopTargeting(){
		target = null;
		isTargeting = false;
		Destroy (targetCursor);
	}

}
