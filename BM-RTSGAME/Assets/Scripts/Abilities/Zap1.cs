using UnityEngine;
using System.Collections;

public class Zap1 : DirectTarget {

	public ParticleSystem zapEffect;


	// Use this for initialization
	public void Start () {
		base.Start ();

		//Debug.Log ("START FROM ZAP");
		keyToUse = KeyCode.Z;
		cost = 50;
		damage = 100;
		range = 10f;

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("ZAP EFFECT: "+zapEffect);

		//Debug.Log (isTargeting);

		base.Update ();

		if (isTargeting) {
			if (Input.GetMouseButtonDown (0)) {
				//Debug.Log("MOUSE CLICK!");

				if (caster.GetComponent<Unit> ().memory - cost < 0) {
					Debug.Log("NOT ENOUGH memory to cast");
					return;
				}

				if(dist > range){
					//Debug.Log("OUT OF RANGE!");
					return;
				}
				if (GUIUtility.hotControl == 0) { //Check if there is a GUI Element under the mouse. If not, continue with the Raycasting.
					Vector3 mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					RaycastHit hit;
					LayerMask layermaskU = (1 << 12);
					if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f, layermaskU)) {
						zap(hit.transform.gameObject);
					}
				}
			}
		}
	}

	
	void OnGUI(){
		if(caster.GetComponent<Unit>().isSelected){
			float[] coord = guiScript.GetButtonCoordinates (0,1);
			
			//Debug.Log("FROM BUILDING! "+guiScript.GetButtonCoordinates ()[0]+ " "+coord[0]);
			if(GUI.Button (new Rect (coord[0], coord[1], coord[2], coord[3]), "ZAP2")){
				Do ();
			}
		}
	}


	public override bool Do(){
		Debug.Log ("ZAP FROM UNIT 1");
		if (!base.Do ()) {
			//Debug.Log(base.Do ());
			return false;		
		}

		//Debug.Log ("I'M READY TO ZAP");
		if (isTargeting) {
			return false;		
		}
		isTargeting = true;
		return true;
	}

	public void zap(GameObject t){
		//Debug.Log ("YOU GOT ZAPPED!");
		target = t;
		caster.GetComponent<Unit>().TakeMemory (cost);
		t.GetComponent<Unit>().health -= damage;
		zapEffect = (ParticleSystem)Instantiate (zapEffect);
		zapEffect.transform.position = targetCursor.transform.position;
		zapEffect.Play ();
		//if (zapEffect.isPlaying)
		//Debug.Log ("IS PLAYING PARTICLES");

		StopTargeting ();
		aMan.listOfAbilities.Clear ();
	}
}
